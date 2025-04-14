using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class AccountRepository : IAccountRepository{
        private readonly string _connectionString;
        private readonly string _schema;
        public AccountRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public List<Company> GetCompanies(){
            List<Company> companies = new List<Company>();
            string query = @$"SELECT [Name] FROM [Company]";
            
            try{
                using(SqlConnection conn = new SqlConnection(_connectionString)){
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn)){

                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.HasRows){
                                while(reader.Read()){
                                    Company item = new Company{
                                        CompanyName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                    };
                                    companies.Add(item);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }catch{

            }

            return companies;
        }

        public WebLoginUser GetUserByName(string name, string password){
            WebLoginUser user = new WebLoginUser();
            string query = @$"SELECT [User ID], [Password], [Web Visibility] 
            FROM [{_schema}$Light User] 
            WHERE [User ID] = @name AND [Password] = @password";
            
            try{
                using(SqlConnection conn = new SqlConnection(_connectionString)){
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn)){
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@password", password);

                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.Read()){
                                user.UserId = reader["User ID"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.WebVisibility = Convert.ToInt32(reader["Web Visibility"]);
                            }
                        }
                    }
                    conn.Close();
                }
            }catch{

            }

            return user;
        }
    }
}