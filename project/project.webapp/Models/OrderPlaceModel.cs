using project.entity;

namespace project.webapp.Models{
    public class OrderPlaceModel
    {
        public List<Item> Items { get; set; }
        public List<Item> TonerItems { get; set; }
        public PageModel PageModel { get; set; }
        public int ModalCurrentPage { get; set; } = 0; //0 home, 1 part and accessories, 2 material
    }
}

