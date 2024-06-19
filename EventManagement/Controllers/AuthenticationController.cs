using Entity.Identity;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using OptionsSetup.Authentication;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJwtProvider _jwtProvider;
        /*       private readonly HttpClient _httpClient;*/

        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        RoleManager<ApplicationRole> roleManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        IHttpContextAccessor contextAccessor,
                                        IJwtProvider jwtProvider
                                        /*HttpClient httpClient*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _jwtProvider = jwtProvider;
            /*_httpClient = httpClient;*/
        }

        [HttpPost]
        [Route("roles/add")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO request)
        {
            var appRole = new ApplicationRole { Name = request.Role };
            var createRole = await _roleManager.CreateAsync(appRole);
            return Ok(new { message = "Role created success" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var result = await RegisterAsync(request);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        private async Task<RegisterResponse> RegisterAsync(RegisterDTO request)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if (userExists != null) return new RegisterResponse { Message = "User already exists", Success = false };

                userExists = new ApplicationUser
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    UserName = request.Email,
                };
                var createUserResult = await _userManager.CreateAsync(userExists, request.Password);
                if (!createUserResult.Succeeded) return new RegisterResponse { Message = $"Create user failed{createUserResult?.Errors?.First()?.Description}", Success = false };
                //user is created
                //add user to role
                var addUserToRoleResult = await _userManager.AddToRoleAsync(userExists, "USER");

                if (!addUserToRoleResult.Succeeded) return new RegisterResponse { Message = $"Create user succeeded but cannot add user to role{createUserResult?.Errors?.First()?.Description}", Success = false };

                //if add role succeed

                return new RegisterResponse
                {
                    Success = true,
                    Message = "User create succesfully"
                };

            }
            catch (Exception ex)
            {
                return new RegisterResponse { Message = ex.Message, Success = false };
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var result = await LoginAsync(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        private async Task<LoginResponse> LoginAsync(LoginDTO request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                /*if (user is null) */
                if (result.Succeeded)
                {
                    var tokenString = _jwtProvider.GenerateToken(user);
                    _contextAccessor.HttpContext.Response.Cookies.Append("Bearer", tokenString, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),
                        HttpOnly = true
                    });
                    return new LoginResponse
                    {
                        AccessToken = tokenString,
                        Message = "Login Successful",
                        Email = user?.Email,
                        Success = true,
                        UserId = user?.Id.ToString()
                    };
                }
                else
                {
                    return new LoginResponse { Message = "Invalid email/password", Success = false };
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new LoginResponse { Success = false, Message = ex.Message };
            }
        }
    }

    /*var roles = await _userManager.GetRolesAsync(user);
               var roleClaims = roles.Select(C => new Claim(ClaimTypes.Role, C));
               claims.AddRange(roleClaims);*/
}
