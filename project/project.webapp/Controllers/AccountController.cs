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
        private readonly IUserRepository _userRepository;

        public AccountController(ILogger<AccountController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        { //Geçici Bölüm
            HttpContext.Session.SetInt32("UserRole", 3);
            HttpContext.Session.SetString("UserId", "BAŞAK"); //BAŞAK , EKOSIS, İZMİR
            return RedirectToAction("Index", "Home");
            // return View();
        }

        [HttpPost]
        public IActionResult Login(string name, string password, int company)
        {
            var user = new User();
            if(company == 1){
                user = _userRepository.GetUserByName(name, password);
            }else{
                
            }

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
