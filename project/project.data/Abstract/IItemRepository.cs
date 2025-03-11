using project.entity;

namespace project.data.Abstract{
    public interface IItemRepository{
        public List<Item> GetAll(int currentPage, int itemPerPage);
        public List<Item> GetItemsByCategory(int currentPage, int itemPerPage, string selectedItemCode );
        public List<KeyValueItem> GetItemCategories();
        public List<KeyValueItem> GetItemCategories(string SelectedItemCode);
        
        public ItemSerial GetItemBySerialNo(string SerialNo);
    }
}