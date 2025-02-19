using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class UserRepository : IUserRepository{
        private readonly string _connectionString;
        private static string schema;
        public UserRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            schema = "Bilgitas";
        }

        public User GetUserByName(string name, string password){
            User user = null;
            string query = $"SELECT [User ID], [Password], [Web Visibility] from [{schema}$Light User] WHERE [User ID] = @name AND [Password] = @password";
            
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

            if(user == null)
                return new User();

            return user;
        }
    }
}