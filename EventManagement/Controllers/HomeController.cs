using DAL;
using Entity;
using EventManagement.DTO;
using EventManagement.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EventManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _httpClient = clientFactory.CreateClient();
            _contextAccessor = contextAccessor;
            _httpClient.BaseAddress = new Uri("https://localhost:44364");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Check()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login1(string Email, string Password) 
        {
            string apiEndpoint = "api/authenticate/login";

            var loginDto = new LoginDTO { Email = Email, Password = Password };

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            try
            {
                // Make the HTTP POST request
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, content);

                // Ensure the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseData = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseData);

                    if (loginResponse.Success)
                    {
                        _contextAccessor.HttpContext.Response.Cookies.Append("Bearer", loginResponse.AccessToken, new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7),
                            HttpOnly = true
                        });

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, loginResponse.Message);
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                    return View();
                }

            }
            catch (HttpRequestException e)
            {
                // Handle exception (log it, rethrow it, or return an error view)
                ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string email, string fullname, string password,string confirmpassword)
        {
            string apiEndpoint = "api/authenticate/login";

            var registerDto = new RegisterDTO { Email = email, Password = password, FullName = fullname, ConfirmPassword = confirmpassword };

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            try
            {
                // Make the HTTP POST request
                HttpResponseMessage response = await _httpClient.PostAsync(apiEndpoint, content);

                // Ensure the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseData = await response.Content.ReadAsStringAsync();
                    var registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(responseData);

                    if (registerResponse.Success)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, registerResponse.Message);
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                    return View();
                }

            }
            catch (HttpRequestException e)
            {
                // Handle exception (log it, rethrow it, or return an error view)
                ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                return View();
            }
        }


        public IActionResult UserCheck()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
