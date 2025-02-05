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
        private readonly AppDbContext _context;

        public AccountController(ILogger<AccountController> logger, IUserRepository userRepository, AppDbContext context)
        {
            _logger = logger;
            _userRepository = userRepository;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            // var user = _userRepository.GetUserByName(name);
            var user = _context.Users.FirstOrDefault(i=>i.Name == name);

            if (user != null && SecurityHelper.VerifyPassword(user.Password, password))
            {
                HttpContext.Session.SetString("UserRole", user.Role);
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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string password)
        {
            var hashedPassword = SecurityHelper.HashPassword(password);
            var user = new User { Name = name, Password = hashedPassword, Role = "User", Visible = false, Deleted = false };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
