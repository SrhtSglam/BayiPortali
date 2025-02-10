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
            var items = _itemRepository.GetAll().Data;
            
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

