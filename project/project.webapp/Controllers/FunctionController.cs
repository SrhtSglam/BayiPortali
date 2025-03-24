using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.data.Abstract;
using project.entity;
using project.webapp.Filters;
using project.webapp.Models;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClosedXML.Excel;  // ClosedXML kütüphanesini kullanıyoruz

namespace project.webapp.Controllers
{
    [CustomAuthorize(3, 2, 1)]
    public class FunctionController : Controller
    {
        private readonly ILogger<FunctionController> _logger;
        private readonly IOtherRepository _otherRepository;
        private readonly IFunctionRepository _functionRepository;

        public FunctionController(ILogger<FunctionController> logger, IFunctionRepository functionRepository, IOtherRepository otherRepository)
        {
            _logger = logger;
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
        public IActionResult SerialControl(string SerialNo = "")
        {
            var item = _functionRepository.GetItemBySerialNo(SerialNo);
            
            if(item != null && SerialNo != "")
                ViewBag.QuerySuccess = true;
            else
                ViewBag.QuerySuccess = false;
            
            return View(item);
        }

        public IActionResult NumaratorEntry()
        {
            var items = ViewData["NumeratorItems"] as List<NumeratorItem>;

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            List<NumeratorItem> numeratorItems = new List<NumeratorItem>();

            if (file != null && file.Length > 0)
            {
                // Dosyayı bir Stream'e kaydediyoruz
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    try
                    {
                        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
                        System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                        using (var workbook = new XLWorkbook(tempFilePath))
                        {
                            
                            var worksheet = workbook.Worksheet(1);
                            
                            if(worksheet.Name != "NUMARATOR" || worksheet.Name != "NUMARATÖR" || worksheet.Name != ""){
                                RedirectToAction("NumaratorEntry");
                            }else{
                                int rowCount = worksheet.RowsUsed().Count();

                                for (int row = 2; row <= rowCount; row++)
                                {
                                    var numeratorItem = new NumeratorItem
                                    {
                                        BranchName = worksheet.Cell(row, 1).Value?.ToString(),
                                        InventoryNo = worksheet.Cell(row, 2).Value?.ToString(),
                                        ResponsibilityCenter = worksheet.Cell(row, 3).Value?.ToString(),
                                        ContractNo = worksheet.Cell(row, 4).Value?.ToString(),
                                        EndDate = DateOnly.TryParse(worksheet.Cell(row, 5).Value?.ToString() ?? "", out var endDate) ? endDate : default,
                                        ContractStatus = worksheet.Cell(row, 6).Value?.ToString(),
                                        ArticleNo = worksheet.Cell(row, 7).Value?.ToString(),
                                        Description = worksheet.Cell(row, 8).Value?.ToString(),
                                        SerialNo = worksheet.Cell(row, 9).Value?.ToString(),
                                        CurrentDefinition = worksheet.Cell(row, 10).Value?.ToString(),
                                        StockReading = worksheet.Cell(row, 11).Value?.ToString(),
                                        StockEntered = worksheet.Cell(row, 12).Value?.ToString(),
                                        StockDifference = worksheet.Cell(row, 13).Value?.ToString(),
                                        ColoredReading = worksheet.Cell(row, 14).Value?.ToString(),
                                        ColoredEntered = worksheet.Cell(row, 15).Value?.ToString(),
                                        ColoredDifference = worksheet.Cell(row, 16).Value?.ToString(),
                                        StockPrice = worksheet.Cell(row, 17).Value?.ToString(),
                                        RNKPrice = worksheet.Cell(row, 18).Value?.ToString(),
                                        InterventionTime = worksheet.Cell(row, 19).Value?.ToString(),
                                        ResolutionTime = worksheet.Cell(row, 20).Value?.ToString(),
                                        PeriodicMaintenanceTime = worksheet.Cell(row, 21).Value?.ToString(),
                                        Notes = worksheet.Cell(row, 22).Value?.ToString(),
                                        LaborCost = worksheet.Cell(row, 23).Value?.ToString(),
                                        ContractType = worksheet.Cell(row, 24).Value?.ToString(),
                                        ScopeType = worksheet.Cell(row, 25).Value?.ToString(),
                                        ScopeTypeDescription = worksheet.Cell(row, 26).Value?.ToString(),
                                        ServiceBlock = worksheet.Cell(row, 27).Value?.ToString(),
                                        MaterialBlock = worksheet.Cell(row, 28).Value?.ToString(),
                                        PenalClause = worksheet.Cell(row, 29).Value?.ToString(),
                                        CurrencyType = worksheet.Cell(row, 30).Value?.ToString(),
                                        ExchangeRate = worksheet.Cell(row, 31).Value?.ToString()
                                    };

                                    numeratorItems.Add(numeratorItem);
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Hata: " + ex.Message;
                        return View();
                    }
                }

                return View("NumaratorEntry", numeratorItems); 
            }
            else
            {
                ViewBag.Message = "Geçerli bir dosya seçiniz.";
            }

            return RedirectToAction("NumaratorEntry");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
