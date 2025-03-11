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
        private readonly IOtherRepository _otherRepository;
        private readonly IFunctionRepository _functionRepository;

        public FunctionController(ILogger<FunctionController> logger, IItemRepository itemRepository, IFunctionRepository functionRepository, IOtherRepository otherRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
            _otherRepository = otherRepository;
            _functionRepository = functionRepository;
        }

        public IActionResult SellOutEntry(int page = 1)
        {
            int itemPerPage = 40;
            var items = _functionRepository.GetSellOutItems(page, itemPerPage);
            var keyvalueitems = _functionRepository.GetTaxAreas();
            
            int totalPage = _otherRepository.GetCountPerPage("Sell-out Ledger Entry", itemPerPage);
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = page;

            var model = new SelloutModel{
                SellOutItems = items,
                KeyValueItems = keyvalueitems
            };

            return View(model);
        }

        public IActionResult SerialControl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SerialControl(string SerialNo = ""){
            var item = _itemRepository.GetItemBySerialNo(SerialNo);
            
            if(item != null && SerialNo != "")
                ViewBag.QuerySuccess = true;
            else
                ViewBag.QuerySuccess = false;
            
            return View(item);
        }

        public IActionResult NumaratorEntry(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

