namespace project.entity{
    public class SubCategory{
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
    }
}