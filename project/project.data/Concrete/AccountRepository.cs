using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class AccountRepository : IAccountRepository{
        private readonly string _connectionString;
        private string _schema;
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

        public WebLoginUser GetUserByName(string name, string password, string company){
            WebLoginUser user = new WebLoginUser();
            _schema = company;
            string query = @$"SELECT [User ID], [Password], [Web Visibility], [E-mail], [Customer No_]
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
                                user.UserId = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                                user.Password = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                user.WebVisibility = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                WebLoginUser.EMail = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                // WebLoginUser.CustomerNo = reader["Customer No_"].ToString();
                                WebLoginUser.CustomerNo = reader.IsDBNull(reader.GetOrdinal("Customer No_")) ? string.Empty : reader["Customer No_"].ToString();
                            }
                        }
                    }
                    conn.Close();
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }

            // if(string.IsNullOrEmpty(user.UserId))
            //     return user;
            // else{
            //     return null;
            // }
            return user;
        }
    }
}