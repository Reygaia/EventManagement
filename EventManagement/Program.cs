using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using DAL;
using Entity;
using EventManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(_ =>
new UnitOfWork(builder.Configuration,"EventManagement")
);

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

//setup identity options (change to another file later)
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = builder.Configuration.GetConnectionString("MongoDB"),
        DatabaseName = "EventManagement"
    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 1;
        options.Password.RequireNonAlphanumeric = false;

        //lockout
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(168);
        options.Lockout.MaxFailedAccessAttempts = 10;

        options.User.RequireUniqueEmail = false;
    }
};

/*
 this is one hash md5
 */


builder.Services.AddSingleton<DatabaseChecker>();


//add session for future services usage (change to another file later)
builder.Services.AddSession(options =>
{
    // Set session timeout and other options as needed
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // make the session cookie essential
});

builder.Services.AddControllersWithViews();


//setup mongodbidentity 
builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddDefaultTokenProviders();


//setup authentication
//jwt bearer, jwt token checker (change to another file later and revamp proper setup option with appsettings.json) 
//use cookie to store and check token data
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.Cookie.Name = "Bearer";

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = "https://localhost:44364",
        ValidAudience = "https://localhost:44364",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("984925f60df9b037cff0e784dfc013f2984925f60df9b037cff0e784dfc013f2")),
        ClockSkew = TimeSpan.Zero,
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Bearer"];
            return Task.CompletedTask;
        }
    };
});

/*builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
});*/

var app = builder.Build();


var databaseChecker = app.Services.GetRequiredService<DatabaseChecker>();
databaseChecker.CheckConnection("EventManagement");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
