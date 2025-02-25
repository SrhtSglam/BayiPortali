using project.entity;

namespace project.data.Abstract{
    public interface IAccountRepository{
        public List<Company> GetCompanies();
        public User GetUserByName(string name, string password, string company);
    }
}