using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.webapp.Filters;
using project.webapp.Models;

namespace project.webapp.Controllers{
    [CustomAuthorize(3, 2, 1)]
    public class FunctionController : Controller
    {
        private readonly ILogger<FunctionController> _logger;
        private readonly IItemRepository _itemRepository;

        public FunctionController(ILogger<FunctionController> logger, IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        public IActionResult SellOutEntry()
        {
            return View();
        }

        public IActionResult SerialControl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SerialControl(string SerialNo = ""){
            var item = _itemRepository.GetItemBySerialNo(SerialNo);
            
            if(item != null && SerialNo != "" && SerialNo != null)
                ViewBag.QuerySuccess = true;
            else
                ViewBag.QuerySuccess = false;
            
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

