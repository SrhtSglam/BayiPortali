using project.entity;

namespace project.data.Abstract{
    public interface IOrderRepository{
        public ItemDetail GetItemDetail(string itemCode);
        public List<BasketItem> GetBasketByUserId(int ConfirmedStatus);
    }
}