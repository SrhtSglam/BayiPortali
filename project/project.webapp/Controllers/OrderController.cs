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
        private readonly IOtherRepository _otherRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderController(ILogger<OrderController> logger, IOtherRepository otherRepository, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _otherRepository = otherRepository;
        }

        public IActionResult OrderHome()
        {
            return View();
        }

        public IActionResult Place(int page = 1)
        {
            int pageSize = 40;
            
            var items = _orderRepository.GetAll(page, pageSize);
            var itemCategories = _orderRepository.GetItemCategories();

            int totalPage = _otherRepository.GetCountPerPage("Item", pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = page;

            var model = new OrderPlaceModel
            {
                Items = items,
                ItemCategories = itemCategories,
                TotalPages = totalPage,
                CurrentPage = page
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetItemCategoriesByItemCode(string selectedItemCode)
        {
            var filteredCategories = _orderRepository.GetItemCategories(selectedItemCode);
            return Json(filteredCategories);
        }

        [HttpGet]
        public JsonResult GetItemsByCategory(string selectedItemCode)
        {
            var filteredCategories = _orderRepository.GetItemsByCategory(1, 40, selectedItemCode);
            return Json(filteredCategories);
        }

        [HttpGet]
        public JsonResult GetItemDetails(string itemCode)
        {
            var item = _orderRepository.GetItemDetail(itemCode);
            return Json(item);
        }

        public IActionResult Confirm(){
            var items = _orderRepository.GetBasketByUserId(0);
            return View(items);
        }

        public IActionResult Control(){
            var items = _orderRepository.GetBasketByUserId(1);
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

