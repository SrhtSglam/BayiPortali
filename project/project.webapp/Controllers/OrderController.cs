using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.entity;
using project.webapp.Filters;
using project.webapp.Models;
using project.webapp.Services;

namespace project.webapp.Controllers{
    [CustomAuthorize(3,2,1)]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOtherRepository _otherRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderController(ILogger<OrderController> logger, IOtherRepository otherRepository, IOrderRepository orderRepository, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _orderRepository = orderRepository;
            _otherRepository = otherRepository;
        }

        public IActionResult OrderHome()
        {
            return View();
        }

        public IActionResult CreateOrder(string _selectedItemCode, string _selectedSubItemCode, int page = 1)
        {
            int pageSize = 40;
            
            var items = _orderRepository.GetAll(page, pageSize, _selectedItemCode, _selectedSubItemCode);

            var itemCategories = _orderRepository.GetItemCategories();

            int totalCount = _orderRepository.GetFilterItemCount(_selectedItemCode, _selectedSubItemCode);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.TotalPage = totalPages;
            ViewBag.CurrentPage = page;

            ViewBag.SelectedItemCode = _selectedItemCode;
            ViewBag.SelectedSubItemCode = _selectedSubItemCode;

            bool useDynamicImage = _configuration.GetValue<bool>("UseDynamicImage");
            ViewBag.UseDynamicImage = useDynamicImage;

            var model = new OrderPlaceModel
            {
                Items = items,
                ItemCategories = itemCategories,
                TotalPages = totalPages,
                CurrentPage = page
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToBasket(string pItemNo, decimal pQuantity, string pSalesDescription)
        {
            if (string.IsNullOrEmpty(pItemNo) || pQuantity <= 0)
            {
                Console.WriteLine("Geçersiz veri!");
                // return RedirectToAction("CreateOrder");
                return Json(new { success = false, message = "Geçersiz veri!" });
            }

            // Console.WriteLine(DateTime.Now + " " + WebLoginUser.AuthId + " " + pItemNo + " " + pQuantity + " " + pSalesDescription);
            // var result = await NAVServer.InsertWebBasket(DateTime.Now, WebLoginUser.AuthId, "04KYM4125IDN", pQuantity, pSalesDescription);
            // if (result.success)
            // {
            //     TempData["PopupMessage"] = "Ürün başarıyla sepete eklendi.";
            // }
            // else
            // {
            //     TempData["PopupMessage"] = "Ürün sepete eklenemedi.";
            // }
            TempData["PopupMessage"] = "Ürün başarıyla sepete eklendi.";
            // return Json(new { success = true, message = "Ürün başarıyla sepete eklendi." });
            return RedirectToAction("CreateOrder");
        }

        [HttpGet]
        public JsonResult GetItemCategoriesByItemCode(string selectedItemCode, int page = 1)
        {
            int pageSize = 40;
            var filteredCategories = _orderRepository.GetItemCategories(selectedItemCode);
            var items = _orderRepository.GetItemsByCategory(page, pageSize, selectedItemCode, null);
            var totalCount = _orderRepository.GetFilterItemCount(selectedItemCode, null);
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return Json(new
            {
                filteredItems = items,
                categories = filteredCategories,
                totalPages = totalPages,
                currentPage = page
            });
        }


        [HttpGet]
        public JsonResult GetItemsByCategory(string selectedItemCode, string selectedSubCategory)
        {
            int page = 1;
            int pageSize = 40;

            var filteredItems = _orderRepository.GetItemsByCategory(page, pageSize, selectedItemCode, selectedSubCategory);
            var totalCount = _orderRepository.GetFilterItemCount(selectedItemCode, selectedSubCategory);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return Json(new { items = filteredItems, totalPages = totalPages });
        }

        [HttpGet]
        public JsonResult GetItemsByCategoryPaged(string selectedItemCode, string selectedSubCategory, int page = 1)
        {
            int pageSize = 40;

            var filteredItems = _orderRepository.GetItemsByCategory(page, pageSize, selectedItemCode, selectedSubCategory);
            var totalCount = _orderRepository.GetFilterItemCount(selectedItemCode, selectedSubCategory);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return Json(new { items = filteredItems, totalPages = totalPages, currentPage = page });
        }

        [HttpGet]
        public JsonResult GetItemDetails(string itemCode)
        {
            var itemDetailList = _orderRepository.GetItemDetail(itemCode);
            var itemComponentList = _orderRepository.GetComponentsByItemNo(itemCode);
            return Json(new { itemDetailData = itemDetailList, itemComponentData = itemComponentList});
        }

        [HttpGet]
        public JsonResult GetAllFilterItems(string pItemCode)
        {
            var filteredItems = _orderRepository.GetAll(1, 40, pItemCode);

            return Json(filteredItems);
        }

        public IActionResult Confirm(){
            var items = _orderRepository.GetBasketByUserId(0);
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBasket()
        {
            await NAVServer.ConfirmWebBasket(WebLoginUser.AuthId);
            return RedirectToAction("Place");
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

