using project.entity;

namespace project.webapp.Models{
    public class OrderPlaceModel
    {
        public List<Item> Items { get; set; }
        public List<KeyValueItem> ItemCategories { get; set; }
        public List<KeyValueItem> ItemCategoriesById { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        
    }
}

