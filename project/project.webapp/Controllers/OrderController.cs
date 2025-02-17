using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
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

        public IActionResult Place()
        {
            var items = _itemRepository.GetAll(1, 20);
            var itemCategories = _itemRepository.GetItemCategories();

            int totalPage = _itemRepository.GetCount(20);
            ViewBag.TotalPage = totalPage;
            
            var model = new OrderPlaceModel{
                Items = items.Data,
                ItemCategories = itemCategories.Data,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Place(int ModalCurrentPage)
        {
            var items = _itemRepository.GetAll(ModalCurrentPage, 20);
            var itemCategories = _itemRepository.GetItemCategories();
            var itemCategoriesChild = _itemRepository.GetItemCategoriesWithChild("2EL.004");

            int totalPage = _itemRepository.GetCount(20);
            ViewBag.TotalPage = totalPage;
            
            var model = new OrderPlaceModel{
                Items = items.Data,
                ItemCategories = itemCategories.Data,
                ItemCategoriesWithChild = itemCategoriesChild.Data
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

