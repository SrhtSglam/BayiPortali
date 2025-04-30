using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.webapp.Models;
using project.entity;
using project.webapp.Filters;
using project.data.Abstract;
using project.data;
using project.data.Concrete;
using project.webapp.Services;
using System.Threading.Tasks;

namespace project.webapp.Controllers
{
    public class DevController : Controller
    {
        private readonly ILogger<DevController> _logger;

        public DevController(ILogger<DevController> logger)
        {
            _logger = logger;
        }

        public IActionResult NavTest()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NavTest(string pUserId, string pItemNo, decimal pQuantity, string pSalesDescription)
        {
            try{
                if(NAVServer.mWebManagement == null){
                    ViewBag.Message = "_webmanagement is null";
                }
                else{
                    await NAVServer.InsertWebBasket(DateTime.Now, pUserId, pItemNo, pQuantity, pSalesDescription);
                    ViewBag.Message = "Veri başarılı bir şekilde gönderildi";
                }
            }catch(Exception ex){
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }
}
