using project.entity;

namespace project.data.Abstract{
    public interface IOrderRepository{
        public ItemDetail GetItemDetail(string itemCode);
        public List<BasketItem> GetBasketByUserId(int ConfirmedStatus);
        public Task<List<Item>> GetAllAsync(int currentPage, int itemPerPage, string selectedItemCode, string selectedSubItemCode);
        public int GetFilterItemCount(string selectedItemCode, string selectedSubItemCode);
        public List<Item> GetAll(int currentPage, int itemPerPage,string itemCode, string description, string alternativeCode);
        public List<KeyValueItem> GetItemCategories();
        public List<KeyValueItem> GetItemCategories(string SelectedItemCode);
        public List<Item> GetItemsByCategory(int currentPage, int itemPerPage, string selectedItemCode, string selectedSubCategory);
        public List<ItemComponent> GetComponentsByItemNo(string itemNo);

        public Task DeleteItemFromBasket(DateTime pDateTime, string pItemNo);
    }
}