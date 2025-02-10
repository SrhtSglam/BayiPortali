using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.data.Concrete;
using project.webapp.Filters;
using project.webapp.Models;

namespace project.webapp.Controllers{
    [CustomAuthorize("Admin", "User")]
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
            var pagination = new PageModel(){
                CurrentPage = 1,
                ItemPerPage = 20
            };
            var items = _itemRepository.GetAll(pagination.CurrentPage, pagination.ItemPerPage).Data;
            var toners = _itemRepository.GetAllToner(pagination.CurrentPage, pagination.ItemPerPage).Data;

            var model = new OrderPlaceModel(){
                Items = items,
                TonerItems = toners,
                ModalCurrentPage = 0,
                PageModel = pagination
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Place(int ModalCurrentPage){
            var pagination = new PageModel(){
                CurrentPage = 1,
                ItemPerPage = 20
            };
            var items = _itemRepository.GetAll(pagination.CurrentPage, pagination.ItemPerPage).Data;
            var toners = _itemRepository.GetAllToner(pagination.CurrentPage, pagination.ItemPerPage).Data;

            var model = new OrderPlaceModel(){
                Items = items,
                TonerItems = toners,
                ModalCurrentPage = ModalCurrentPage,
                PageModel = pagination
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

