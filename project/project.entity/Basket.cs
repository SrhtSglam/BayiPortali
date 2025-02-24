namespace project.entity{
    public class Basket
    {
        public DateTime Date { get; set; }
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string? SalesDescription { get; set; }
        public string Quantity { get; set; }
        public string BaseUnit { get; set; }
    }

    public class BasketItem
    {
        public DateTime Date { get; set; }
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string? SalesDescription { get; set; }
        public decimal Quantity { get; set; }
        public string BaseUnit { get; set; }

        public string? FormattedQuantity { get; set; }
    }
}

