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
            string query = @$"SELECT [User ID], [Password], [Web Visibility], [E-mail], [Customer No_],
            [Sipariş], [Sipariş Geç], [Sipariş Kontrol], [Sipariş Onayla],
            [İşlevler], [Sell-out Giriş], [Numaratör Giriş], [Seri Kontrol], [Garanti], [Garanti İstek Giriş], [Garanti Süreç Takibi],
            [Raporlar], [Mizan], [Ekstre],
            [Duyuru Düzenle], [Download]
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
                                WebLoginUser.CustomerNo = reader.IsDBNull(reader.GetOrdinal("Customer No_")) ? string.Empty : reader["Customer No_"].ToString();

                                WebLoginUser.PageAuthorize.Siparis = reader.IsDBNull(reader.GetOrdinal("Sipariş")) ? false : Convert.ToBoolean(reader["Sipariş"]);
                                WebLoginUser.PageAuthorize.Siparis_Gec = reader.IsDBNull(reader.GetOrdinal("Sipariş Geç")) ? false : Convert.ToBoolean(reader["Sipariş Geç"]);
                                WebLoginUser.PageAuthorize.Siparis_Kontrol = reader.IsDBNull(reader.GetOrdinal("Sipariş Kontrol")) ? false : Convert.ToBoolean(reader["Sipariş Kontrol"]);
                                WebLoginUser.PageAuthorize.Siparis_Onayla = reader.IsDBNull(reader.GetOrdinal("Sipariş Onayla")) ? false : Convert.ToBoolean(reader["Sipariş Onayla"]);
                                WebLoginUser.PageAuthorize.Islevler = reader.IsDBNull(reader.GetOrdinal("İşlevler")) ? false : Convert.ToBoolean(reader["İşlevler"]);
                                WebLoginUser.PageAuthorize.Islevler_SellOutGiris = reader.IsDBNull(reader.GetOrdinal("Sell-out Giriş")) ? false : Convert.ToBoolean(reader["Sell-out Giriş"]);
                                WebLoginUser.PageAuthorize.Islevler_NumaratorGiris = reader.IsDBNull(reader.GetOrdinal("Numaratör Giriş")) ? false : Convert.ToBoolean(reader["Numaratör Giriş"]);
                                WebLoginUser.PageAuthorize.Islevler_SeriKontrol = reader.IsDBNull(reader.GetOrdinal("Seri Kontrol")) ? false : Convert.ToBoolean(reader["Seri Kontrol"]);
                                WebLoginUser.PageAuthorize.Islevler_Garanti = reader.IsDBNull(reader.GetOrdinal("Garanti")) ? false : Convert.ToBoolean(reader["Garanti"]);
                                WebLoginUser.PageAuthorize.Islevler_Garanti_IstekGiris = reader.IsDBNull(reader.GetOrdinal("Garanti İstek Giriş")) ? false : Convert.ToBoolean(reader["Garanti İstek Giriş"]);
                                WebLoginUser.PageAuthorize.Islevler_Garanti_SurecTakip = reader.IsDBNull(reader.GetOrdinal("Garanti Süreç Takibi")) ? false : Convert.ToBoolean(reader["Garanti Süreç Takibi"]);
                                WebLoginUser.PageAuthorize.DuyuruDuzenle = reader.IsDBNull(reader.GetOrdinal("Duyuru Düzenle")) ? false : Convert.ToBoolean(reader["Duyuru Düzenle"]);
                                WebLoginUser.PageAuthorize.Download = reader.IsDBNull(reader.GetOrdinal("Download")) ? false : Convert.ToBoolean(reader["Download"]);
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
            
            return user;
        }
    }
}