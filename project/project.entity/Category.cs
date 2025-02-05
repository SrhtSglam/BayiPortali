namespace project.entity{
    public class Category{
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }

    }
}