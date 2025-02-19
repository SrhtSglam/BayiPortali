using project.entity;

namespace project.data.Abstract{
    public interface IUserRepository{
        public User GetUserByName(string name, string password);
    }
}