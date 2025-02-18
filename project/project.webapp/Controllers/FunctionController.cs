using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.webapp.Filters;
using project.webapp.Models;

namespace project.webapp.Controllers{
    [CustomAuthorize(3, 2, 1)]
    public class FunctionController : Controller
    {
        private readonly ILogger<FunctionController> _logger;

        public FunctionController(ILogger<FunctionController> logger)
        {
            _logger = logger;
        }

        public IActionResult SellOutEntry()
        {
            return View();
        }

        public IActionResult SerialControl()
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

