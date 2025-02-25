using project.entity;

namespace project.data.Abstract{
    public interface IFunctionRepository{
        public List<SellOutItem> GetSellOutItems(int currentPage, int itemPerPage);
    }
}