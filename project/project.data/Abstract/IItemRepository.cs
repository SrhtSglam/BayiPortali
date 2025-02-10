using project.entity;

namespace project.data.Abstract{
    public interface IItemRepository{
        public DataResponse<List<Item>> GetAll(int currentPage, int itemPerPage);
    }
}