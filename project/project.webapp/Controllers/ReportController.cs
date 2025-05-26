using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project.entity;
using project.webapp.Filters;
using project.webapp.Models;
using project.webapp.Services;

namespace project.webapp.Controllers{
    [CustomAuthorize("3,2,1")]
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }

        public IActionResult Mizan()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MizanCreateReport(DateTime pStartDate, DateTime pEndDate, string pFileType)
        {
            //pdf xlsx formatında gönderilecek
            var result = await NAVServer.CreateReport_50018(WebLoginUser.CustomerNo, pStartDate, pEndDate, pFileType);
            Console.WriteLine(result.success);

            byte[] fileBytes = Convert.FromBase64String(result.fileContent);
            return File(fileBytes, $"application/{pFileType}", $"{WebLoginUser.CustomerNo}-{pStartDate.ToString("yyyy-MM-dd")}-{pEndDate.ToString("yyyy-MM-dd")}.{pFileType}"); 
        }
        
        public IActionResult Ekstre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EkstreCreateReport(DateTime pStartDate, DateTime pEndDate, string pFileType)
        {
            //pdf xlsx formatında gönderilecek
            var result = await NAVServer.CreateReport_50162(WebLoginUser.CustomerNo, pStartDate, pEndDate, pFileType);
            Console.WriteLine(result.success);

            byte[] fileBytes = Convert.FromBase64String(result.fileContent);
            return File(fileBytes, $"application/{pFileType}", $"{WebLoginUser.CustomerNo}-{pStartDate.ToString("yyyy-MM-dd")}-{pEndDate.ToString("yyyy-MM-dd")}.{pFileType}"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

