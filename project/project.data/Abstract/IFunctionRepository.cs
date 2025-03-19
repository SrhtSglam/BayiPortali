using project.entity;

namespace project.data.Abstract{
    public interface IFunctionRepository{
        public List<SellOutItem> GetSellOutItems(int currentPage, int itemPerPage);
        public List<KeyValueItem> GetTaxAreas();
        public ItemSerial GetItemBySerialNo(string SerialNo);
    }
}