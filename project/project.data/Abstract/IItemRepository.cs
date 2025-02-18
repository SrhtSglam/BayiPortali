using project.entity;

namespace project.data.Abstract{
    public interface IItemRepository{
        public DataResponse<List<Item>> GetAll(int currentPage, int itemPerPage);
        public DataResponse<List<ItemCategory>> GetItemCategoriesWithChild(string itemCode);
        public DataResponse<List<ItemCategory>> GetItemCategories();
        public DataResponse<ItemDetail> GetItemDetail();
        public int GetCount(int itemPerPage);
    }
}