namespace project.entity
{
    public class ItemDetail
    {
        public string ItemNo { get; set; }
        public string? ItemNo2 { get; set; }
        public string Description { get; set; }
        public string? PictureData { get; set; }
        public string? BaseUnitOfMeasure { get; set; }

        public bool Featured { get; set; }  = false;
        public int MonthlyPrintVolume { get; set; }
        public int PrintingCapacity { get; set; }
    }
}