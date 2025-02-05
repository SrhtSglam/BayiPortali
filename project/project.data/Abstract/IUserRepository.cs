using project.entity;

namespace project.data.Abstract{
    public interface IUserRepository{
        public Task<DataResponse<List<User>>> GetAll();
        public Task<DataResponse<User>> GetUserByName(string name);
    }
}