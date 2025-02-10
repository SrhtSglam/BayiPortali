namespace project.entity{
    public class Item{
        public int ItemId { get; set; }
        public string CategoryName { get; set; }
        public string ItemGroup { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string? AlternativeCode { get; set; }
        public string StockTransferType { get; set; }
        public string SalesPrice { get; set; }
        public int StockAmount { get; set; }

    }
}