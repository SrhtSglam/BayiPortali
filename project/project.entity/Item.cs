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
        
        public string? Image { get; set; }
        public string? FormattedPrice { get; set; }
    }

    public class ItemDetail{
        public string? PictureData { get; set; }
        public string? BaseUnitOfMeasure { get; set; }
        public int MonthlyPrintVolume { get; set; }
        public int PrintingCapacity { get; set; }
    }

    public class ItemSerial
    {
        public string? Code { get; set; }
        public string? SerialNo { get; set; }
        public string? Description { get; set; }
    }
}

