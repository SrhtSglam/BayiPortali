using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.webapp.Models;
using project.entity;
using project.webapp.Filters;
using project.data.Abstract;
using project.data;
using project.data.Concrete;

namespace project.webapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;

        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            // WebLoginUser.AuthId = "ERDEN_ADMIN";
            // WebLoginUser.Visibility = 3;
            // WebLoginUser.Company = "Bilgitas";
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
