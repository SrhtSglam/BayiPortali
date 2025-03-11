using project.entity;

namespace project.data.Abstract{
    public interface IAccountRepository{
        public List<Company> GetCompanies();
        public WebLoginUser GetUserByName(string name, string password);
    }
}