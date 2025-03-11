using project.entity;

namespace project.data.Abstract{
    public interface IBasketRepository{
        public List<BasketItem> GetBasketByUserId(int ConfirmedStatus);
    }
}