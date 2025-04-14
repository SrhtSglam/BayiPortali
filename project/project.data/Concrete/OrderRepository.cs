using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class OrderRepository : IOrderRepository{
        private readonly string _connectionString;
        private readonly string _schema;
        public OrderRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public ItemDetail GetItemDetail(string itemCode){
            ItemDetail item = new ItemDetail();
            string query = @$"SELECT [Base Unit of Measure], [Aylık Baskı Hacmi], [Baskı Kapasitesi (Num)], [Picture]
            FROM [{_schema}$Item] WHERE [No_] = @itemCode";
            
            try{
                using(SqlConnection conn = new SqlConnection(_connectionString)){
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn)){
                        cmd.Parameters.AddWithValue("@itemCode", itemCode);
                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.Read()){
                                item = new ItemDetail{
                                    BaseUnitOfMeasure = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                    MonthlyPrintVolume = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                    PrintingCapacity = reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
                                
                                };

                                if (!reader.IsDBNull(3))
                                    {
                                        byte[] pictureData = (byte[])reader[3];
                                        string base64Image = Convert.ToBase64String(pictureData);
                                        item.PictureData = $"data:image/jpeg;base64,{base64Image}";
                                    }
                                    else
                                    {
                                        item.PictureData = string.Empty;
                                    }
                                }
                        }
                    }
                    conn.Close();
                }
            }catch{

            }

            return item;
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

        public List<Item> GetAll(int currentPage, int itemPerPage){
            List<Item> items = new List<Item>();
            int offset = (currentPage - 1) * itemPerPage;
            string query = $@"SELECT BIC.[Description], BI.[Product Group Code], 
            BI.[No_], BI.[Description], BI.[No_ 2], 
            SP.[Currency Code], SP.[Unit Price], BI.[Picture]
            FROM [{_schema}$Item] BI 
            INNER JOIN [{_schema}$Item Category] BIC on 
            BIC.[Code] = BI.[Item Category Code]
            LEFT JOIN [{_schema}$Sales Price] SP on
            SP.[Item No_] = BI.[No_]
            WHERE SP.[Sales Code] = 'BAYI'
            ORDER BY BI.[No_]
            OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY";
            
            try{
                using(SqlConnection conn = new SqlConnection(_connectionString)){
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn)){
                        cmd.Parameters.AddWithValue("@Offset", offset);
                        cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);

                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.HasRows){
                                while(reader.Read()){
                                    Item item = new Item{
                                        ItemCategory = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                        ProductGroup = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                        ItemCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                        ItemDescription = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                        AlternativeCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                        CurrencyCode = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                        UnitPrice = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                                    };

                                    if (!reader.IsDBNull(7))
                                    {
                                        byte[] pictureData = (byte[])reader[7];
                                        // string decodedString = Encoding.UTF8.GetString(pictureData);
                                        string base64Image = Convert.ToBase64String(pictureData);

                                        // string base64Image = System.Text.Encoding.UTF8.GetBytes(pictureData);
                                        // item.Image = $"data:image/jpeg;base64,{base64Image}";
                                        item.Image = base64Image;
                                    }
                                    else
                                    {
                                        item.Image = string.Empty;
                                    }

                                    item.FormattedPrice = item.UnitPrice.ToString("0.##");
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch{
                
            }

            return items;
        }

        public List<KeyValueItem> GetItemCategories(){
            List<KeyValueItem> itemCategories = new List<KeyValueItem>();
            string query = $@"SELECT 
            [Code], [Description]
            FROM [{_schema}$Item Category];";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    
                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            KeyValueItem item = new KeyValueItem{
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if(itemCategories.Count == 0){
                return new List<KeyValueItem>();
            }

            return itemCategories;
        }

        public List<KeyValueItem> GetItemCategories(string SelectedItemCode){
            List<KeyValueItem> itemCategories = new List<KeyValueItem>();
            string query = $@"SELECT 
            [Item Category Code], [Description]
            FROM [{_schema}$Product Group]
            WHERE [Item Category Code] = @itemCode";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@itemCode", SelectedItemCode);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            KeyValueItem item = new KeyValueItem{
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if(itemCategories.Count == 0){
                return new List<KeyValueItem>();
            }

            return itemCategories;
        }

        public List<Item> GetItemsByCategory(int currentPage, int itemPerPage, string selectedItemCode){
            List<Item> items = new List<Item>();
            int offset = (currentPage - 1) * itemPerPage;
            string query = $@"SELECT BIC.[Description], BI.[Product Group Code], 
            BI.[No_], BI.[Description], BI.[No_ 2], 
            SP.[Currency Code], SP.[Unit Price]
            FROM [{_schema}$Item] BI 
            INNER JOIN [{_schema}$Item Category] BIC on 
            BIC.[Code] = BI.[Item Category Code]
            LEFT JOIN [{_schema}$Sales Price] SP on
            SP.[Item No_] = BI.[No_]
            WHERE BIC.[Code] = @SelectItem AND SP.[Sales Code] = 'BAYI'
            ORDER BY BI.[No_]
            OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY";
            
            try{
                using(SqlConnection conn = new SqlConnection(_connectionString)){
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn)){
                        cmd.Parameters.AddWithValue("@Offset", offset);
                        cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);
                        cmd.Parameters.AddWithValue("@SelectItem", selectedItemCode);

                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.HasRows){
                                while(reader.Read()){
                                    Item item = new Item{
                                        ItemCategory = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                        ProductGroup = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                        ItemCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                        ItemDescription = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                        AlternativeCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                        CurrencyCode = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                        UnitPrice = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6)
                                    };
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