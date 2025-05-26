using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.data.Concrete;
using project.entity;
using project.webapp.Filters;
using project.webapp.Models;

namespace project.webapp.Controllers{
    [CustomAuthorize("3, 2, 1")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailHelper _mailHelper;

        public HomeController(ILogger<HomeController> logger, IMailHelper mailHelper)
        {
            _logger = logger;
            _mailHelper = mailHelper;
        }

    public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("UserRole") == 0){
                return RedirectToAction("Login", "Account");
            }

            // _mailHelper.SendMail("Sipariş Onay", "Siparişiniz Onaylanmıştır Başarıyla!"); //Çalışıyor

            return View();
        }
        
        public IActionResult Privacy()
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

