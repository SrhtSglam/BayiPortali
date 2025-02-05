using project.entity;

namespace project.data.Abstract{
    public interface IUserRepository{
        public List<User> GetAll();
    }
}