using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class AccountRepository : IAccountRepository{
        private readonly SchemaService _schemeService;
        private readonly string _connectionString;
        private readonly string _schema;
        public AccountRepository(IConfiguration configuration, SchemaService schemeService){
            _connectionString = configuration.GetConnectionString("Default");
            _schemeService = schemeService;
            _schema = _schemeService.GetSchema();
        }

        public List<Company> GetCompanies(){
            List<Company> companies = new List<Company>();
            string query = @$"SELECT [Name] FROM [Company]";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            Company item = new Company{
                                CompanyName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                            };
                            companies.Add(item);
                        }
                    }
                }
            }

            if(companies == null)
                return new List<Company>();

            return companies;
        }

        public User GetUserByName(string name, string password, string company){
            User user = new User();
            string query = $"SELECT [User ID], [Password], [Web Visibility] from [{_schema}$Light User] WHERE [User ID] = @name AND [Password] = @password";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", password);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if(reader.Read()){
                            user = new User{
                                UserId = reader["User ID"].ToString(),
                                Password = reader["Password"].ToString(),
                                WebVisibility = Convert.ToInt32(reader["Web Visibility"])
                            };
                        }
                    }
                }
            }

            return user;
        }
    }
}