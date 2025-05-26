using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.webapp.Filters;
using project.webapp.Models;

namespace project.webapp.Controllers{
    [CustomAuthorize("3, 2, 1")]
    public class NoticeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public NoticeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    public IActionResult NoticeEdit()
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

