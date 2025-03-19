using project.entity;

namespace project.data.Abstract{
    public interface IOrderRepository{
        public ItemDetail GetItemDetail(string itemCode);
        public List<BasketItem> GetBasketByUserId(int ConfirmedStatus);
        public List<Item> GetAll(int currentPage, int itemPerPage);
        public List<KeyValueItem> GetItemCategories();
        public List<KeyValueItem> GetItemCategories(string SelectedItemCode);
        public List<Item> GetItemsByCategory(int currentPage, int itemPerPage, string selectedItemCode );
    }
}