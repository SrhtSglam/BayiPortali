using project.entity;

namespace project.webapp.Models{
    public class OrderPlaceModel
    {
        public List<Item> Items { get; set; }
        public List<ItemCategory> ItemCategories { get; set; }
        public List<ItemCategory> ItemCategoriesWithChild { get; set; }
    }
}

