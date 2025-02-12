using project.entity;

namespace project.data.Abstract{
    public interface IUserRepository{
        public DataResponse<User> GetUserByName(string name, string password);
    }
}