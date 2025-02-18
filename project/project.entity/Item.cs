namespace project.entity{
    public class Item
    {
        public string? ItemCategory { get; set; }
        public string? ProductGroup { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? AlternativeCode { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class ItemDetail{
        public string BaseUnitOfMeasure { get; set; }
        public string MonthlyPrintVolume { get; set; }
        public string PrintingCapacity { get; set; }
    }

    public class ItemCategory
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}

