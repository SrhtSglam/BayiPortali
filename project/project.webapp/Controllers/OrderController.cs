using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.entity;
using project.webapp.Filters;
using project.webapp.Models;
using project.webapp.Services;

namespace project.webapp.Controllers{
    [CustomAuthorize("3,2,1")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOtherRepository _otherRepository;
        private readonly IOrderRepository _orderRepository;
        private int _PageSize => HttpContext.Session.GetInt32("PageSize") ?? 20;
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

        public async Task<IActionResult> CreateOrder(string _selectedItemCode, string _selectedSubItemCode, int page = 1, int pageSize = 20)
        {
            var items = await _orderRepository.GetAllAsync(page, pageSize, _selectedItemCode, _selectedSubItemCode);
            var itemCategories = _orderRepository.GetItemCategories();

            int totalCount = _orderRepository.GetFilterItemCount(_selectedItemCode, _selectedSubItemCode);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.TotalPage = totalPages;
            ViewBag.CurrentPage = page;
            ViewData["PageSize"] = pageSize;
            HttpContext.Session.SetInt32("PageSize", pageSize);

            ViewBag.PageSizes = new List<int> { 10, 20, 30, 40 };

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
        public async Task<IActionResult> AddToBasket(string pItemNo, decimal pQuantity, string pSalesDescription)
        {
            if (string.IsNullOrEmpty(pItemNo))
            {
                Console.WriteLine("Geçersiz veri!");
                return BadRequest("Geçersiz veri!");
            }
            else if (pQuantity <= 0) {
                Console.WriteLine("Lütfen geçerli adet sayısı giriniz!");
                return BadRequest("Lütfen geçerli adet sayısı giriniz!");
            }

            if (string.IsNullOrEmpty(pSalesDescription))
            {
                pSalesDescription = "-";
            }

            await NAVServer.InsertWebBasket(DateTime.Now, WebLoginUser.AuthId, pItemNo, pQuantity, pSalesDescription);
            
            return Ok("Ürün başarıyla sepete eklendi.");
        }

        [HttpGet]
        public JsonResult GetItemCategoriesByItemCode(string selectedItemCode, int page = 1)
        {
            var filteredCategories = _orderRepository.GetItemCategories(selectedItemCode);
            var items = _orderRepository.GetItemsByCategory(page, _PageSize, selectedItemCode, null);
            var totalCount = _orderRepository.GetFilterItemCount(selectedItemCode, null);
            var totalPages = (int)Math.Ceiling((double)totalCount / _PageSize);

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

            var filteredItems = _orderRepository.GetItemsByCategory(page, _PageSize, selectedItemCode, selectedSubCategory);
            var totalCount = _orderRepository.GetFilterItemCount(selectedItemCode, selectedSubCategory);
            int totalPages = (int)Math.Ceiling((double)totalCount / _PageSize);

            return Json(new { items = filteredItems, totalPages = totalPages });
        }

        [HttpGet]
        public JsonResult GetItemsByCategoryPaged(string selectedItemCode, string selectedSubCategory, int page = 1)
        {
            var filteredItems = _orderRepository.GetItemsByCategory(page, _PageSize, selectedItemCode, selectedSubCategory);
            var totalCount = _orderRepository.GetFilterItemCount(selectedItemCode, selectedSubCategory);
            int totalPages = (int)Math.Ceiling((double)totalCount / _PageSize);

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
        public JsonResult GetAllFilterItems(string pItemCode, string pSalesDescription, string pAlternativeCode)
        {
            var filteredItems = _orderRepository.GetAll(1, _PageSize, pItemCode, pSalesDescription, pAlternativeCode);

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
            return RedirectToAction("CreateOrder");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBasketItem(DateTime pDateTime, string pItemNo)
        {
            await _orderRepository.DeleteItemFromBasket(pDateTime, pItemNo);
            return RedirectToAction("Confirm");
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

