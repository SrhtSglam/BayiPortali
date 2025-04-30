using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class BasketRepository : IBasketRepository{
        private readonly string _connectionString;
        private readonly string _schema;
        public BasketRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public List<BasketItem> GetBasketByUserId(int ConfirmedStatus){
            List<BasketItem> items = new List<BasketItem>();
            string query = @$"SELECT 
            DATEADD(mi, DATEDIFF(mi, GETUTCDATE(), GETDATE()), WB.[Time Stamp]), 
            WB.[Item No_], Item.[Description], WB.[Sales Description], WB.[Quantity], Item.[Base Unit of Measure]
            FROM [{_schema}$Web Basket] WB
            INNER JOIN [{_schema}$Item] Item
            ON WB.[Item No_] = Item.[No_]
            WHERE WB.[User ID] = @UserId AND WB.[Confirmed] = @ConfirmedStatus";
            
            try{
                using(SqlConnection conn = new SqlConnection(_connectionString)){
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn)){
                        cmd.Parameters.AddWithValue("@UserId", WebLoginUser.AuthId);
                        cmd.Parameters.AddWithValue("@ConfirmedStatus", ConfirmedStatus);

                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.HasRows){
                                while(reader.Read()){
                                        BasketItem item = new BasketItem{
                                            Date = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0),
                                            ItemNo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                            Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                            SalesDescription = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                            Quantity = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                                            BaseUnit = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                        };
                                        item.FormattedQuantity = item.Quantity.ToString("0.##");
                                        items.Add(item);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }catch{

            }

            return items;
        }
    }
}