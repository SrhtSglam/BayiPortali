using project.entity;

namespace project.data.Abstract{
    public interface IItemRepository{
        public List<Item> GetAll(int currentPage, int itemPerPage);
        public List<Item> GetItemsByFilter(int currentPage, int itemPerPage, string selectedItemCode );
        public List<ItemCategory> GetItemCategories();
        public List<ItemCategory> GetItemCategories(string SelectedItemCode);
        public ItemDetail GetItemDetail();
        
        public ItemSerial GetItemBySerialNo(string SerialNo);
    }
}