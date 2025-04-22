using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete
{
    public class SMTPRepository : ISMTPRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private string _schema;
        public SMTPRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _configuration = configuration;
            _schema = WebLoginUser.Company;
        }

        public SMTP_Login_Info GetSMTPConfiguration()
        {
            SMTP_Login_Info smtp = new SMTP_Login_Info();
            string query = $@"SELECT 
            SMS.[Primary Key], SMS.[SMTP Server], SMS.[Authentication], SMS.[User ID], SMS.[Password], 
            SMS.[SMTP Server Port], SMS.[Secure Connection], SMS.[Sender Name], SMS.[Sender Address] 
            FROM [{_schema}$SMTP Mail Setup] SMS WHERE SMS.[Primary Key] = @SMTPKey";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SMTPKey", _configuration.GetValue<string>("SMTPKey"));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            smtp = new SMTP_Login_Info
                            {
                                PrimaryKey = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                SMTP_Server = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                Authentication = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                User_Id = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Password = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                SMTP_Server_Port = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                Secure_Connection = reader.IsDBNull(6) ? (byte)0 : reader.GetByte(6),
                                Sender_Name = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                Sender_Address = reader.IsDBNull(8) ? string.Empty : reader.GetString(8)
                            };
                        }
                    }
                }
                conn.Close();
            }
            return smtp;
        }
    }
}