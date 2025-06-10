namespace project.entity{
    public class ItemComponent
    {
        public string? ItemCategory { get; set; }
        public string? ProductGroup { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? AlternativeCode { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal UnitPrice { get; set; }
    
        public string? FormattedPrice { get; set; }
    }
}

