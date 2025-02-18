using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.entity;
using project.webapp.Filters;
using project.webapp.Models;

namespace project.webapp.Controllers{
    [CustomAuthorize(3,2,1)]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IItemRepository _itemRepository;
        public OrderController(ILogger<OrderController> logger, IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        public IActionResult OrderHome()
        {
            return View();
        }

        public IActionResult Place(int page = 1)
        {
            int pageSize = 40;
            
            var items = _itemRepository.GetAll(page, pageSize);
            var itemCategories = _itemRepository.GetItemCategories();

            int totalPage = _itemRepository.GetCount(pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = page; 

            var model = new OrderPlaceModel
            {
                Items = items,
                ItemCategories = itemCategories,
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetItemCategoriesByItemCode(string selectedItemCode)
        {
            try
            {
                var filteredCategories = _itemRepository.GetItemCategories(selectedItemCode);
                return Json(filteredCategories);
            }
            catch (Exception ex)
            {
                return Json(new List<ItemCategory>());
            }
        }

        public IActionResult Confirm(){
            return View();
        }

        public IActionResult Control(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

