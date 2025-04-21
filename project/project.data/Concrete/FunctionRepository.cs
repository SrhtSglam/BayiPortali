using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class FunctionRepository : IFunctionRepository{
        private readonly string _connectionString;
        private readonly string _schema;
        public FunctionRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public List<SellOutItem> GetSellOutItems(int currentPage, int itemPerPage){
            List<SellOutItem> items = new List<SellOutItem>();
            int offset = (currentPage - 1) * itemPerPage;
            string query = @$"SELECT 
            [Entry No_], [Item No_], [Serial No_], [Bilgitaş Invoice No_], [Bilgitaş Invoice Date] 
            FROM [{_schema}$Sell-out Ledger Entry]
            ORDER BY [Entry No_]
            OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            SellOutItem item = new SellOutItem{
                                EntryNo = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                ItemNo = reader.IsDBNull(0) ? string.Empty : reader.GetString(1),
                                SerialNo = reader.IsDBNull(1) ? string.Empty : reader.GetString(2),
                                InvoiceNo = reader.IsDBNull(2) ? string.Empty : reader.GetString(3),
                                InvoiceDate = reader.IsDBNull(3) ? DateTime.Now : reader.GetDateTime(4),
                            };
                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        public List<KeyValueItem> GetTaxAreas(){
            List<KeyValueItem> items = new List<KeyValueItem>();
            string query = @$"SELECT [Code], [Description] FROM [{_schema}$Tax Area]";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if(reader.HasRows){
                            while(reader.Read()){
                                KeyValueItem item = new KeyValueItem{
                                    Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                    Description = reader.IsDBNull(0) ? string.Empty : reader.GetString(1)
                                };
                                items.Add(item);
                            }
                        }
                    }
                }
                conn.Close();
            }

            return items;
        }

        public ItemSerial GetItemBySerialNo(string SerialNo){
            var item = new ItemSerial();
            string query = $@"SELECT 
            ILE.[Item No_], ILE.[Posting Date], ILE.[Serial No_], ILE.[Description] , ILE.[Customer No_], CUS.[Name]
            FROM [{_schema}$Item Ledger Entry] ILE
            INNER JOIN [{_schema}$Customer] CUS ON CUS.No_ = ILE.[Customer No_]
            WHERE ILE.[Serial No_] = @ItemCode and ILE.[Entry Type]=1";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@ItemCode", SerialNo);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if(reader.Read()){
                            item = new ItemSerial{
                                ItemNo = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                PostingDate = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1),
                                SerialNo = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Description = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                CustomerNo = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                Name = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                            };
                        }
                    }
                }
                conn.Close();
            }
            
            return item;
        }
    }
}