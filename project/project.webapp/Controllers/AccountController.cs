using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.webapp.Models;
using project.entity;
using project.webapp.Filters;
using project.data.Abstract;
using project.data;

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
            var companies = _accountRepository.GetCompanies();
            return View(companies);
        }

        [HttpPost]
        public IActionResult Login(string name, string password, string company)
        {
            var user = new User();

            user = _accountRepository.GetUserByName(name, password, company);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserRole", user.WebVisibility);
                HttpContext.Session.SetString("UserId", user.UserId);
                return RedirectToAction("Index", "Home");
            }
 
            ViewBag.Message = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
