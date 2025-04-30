using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.webapp.Models;
using project.entity;
using project.webapp.Filters;
using project.data.Abstract;
using project.data;
using project.data.Concrete;
using project.webapp.Services;
using System.ServiceModel;

namespace project.webapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;

        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            if(_configuration.GetValue<bool>("DevelopmentConfig:UseQuickLogin") == true){
                WebLoginUser.AuthId = "BAŞAK";
                WebLoginUser.Visibility = 3;
                WebLoginUser.Company = "Bilgitas";
            }
            var companies = _accountRepository.GetCompanies();
            return View(companies);
        }

        [HttpPost]
        public IActionResult Login(string name, string password, string company)
        {
            var user = new WebLoginUser();

            user = _accountRepository.GetUserByName(name, password, company);

            if (user != null)
            {
                // HttpContext.Session.SetInt32("UserRole", user.WebVisibility);
                // HttpContext.Session.SetString("UserId", user.UserId);
                WebLoginUser.AuthId = user.UserId;
                WebLoginUser.Visibility = user.WebVisibility;
                WebLoginUser.Company = company;

                NAVServer.NAVServiceURL = _configuration.GetValue<string>("DynamicNAV:NAVServiceURL");
                NAVServer.NAVServiceServerName = _configuration.GetValue<string>("DynamicNAV:NAVServiceServerName");
                NAVServer.NAVServiceCompanyName = _configuration.GetValue<string>("DynamicNAV:NAVServiceCompanyName");
                NAVServer.NAVServiceCodeunit = _configuration.GetValue<string>("DynamicNAV:NAVServiceCodeunit");
                NAVServer.NAVServiceDomain = _configuration.GetValue<string>("DynamicNAV:NAVServiceDomain");
                NAVServer.NAVServiceUserName = _configuration.GetValue<string>("DynamicNAV:NAVServiceUserName");
                NAVServer.NAVServicePassword = _configuration.GetValue<string>("DynamicNAV:NAVServicePassword");

                NAVServer.NAVServiceURL = string.Format(NAVServer.NAVServiceURL, NAVServer.NAVServiceServerName,
                    NAVServer.NAVServiceCompanyName, NAVServer.NAVServiceCodeunit);

                NAVServer.mWebManagement = new WebManagement_PortClient(new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly)
                {
                    Security =
                    {
                        Transport = { ClientCredentialType = HttpClientCredentialType.Basic }
                    },
                    MaxReceivedMessageSize = 20000000
                }, new EndpointAddress(NAVServer.NAVServiceURL));

                NAVServer.mWebManagement.ClientCredentials.UserName.UserName = string.Format("{0}\\{1}", NAVServer.NAVServiceDomain, NAVServer.NAVServiceUserName);
                NAVServer.mWebManagement.ClientCredentials.UserName.Password = NAVServer.NAVServicePassword; 
                
                return RedirectToAction("Index", "Home");
            }
 
            ViewBag.Message = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            WebLoginUser.AuthId = "";
            WebLoginUser.Company = "";
            WebLoginUser.Visibility = 0;
            return RedirectToAction("Login");
        }
    }
}
