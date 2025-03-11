using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class ItemRepository : IItemRepository{
        private readonly string _connectionString;  
        private readonly string _schema;
        public ItemRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public List<Item> GetAll(int currentPage, int itemPerPage){
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

        public ItemSerial GetItemBySerialNo(string SerialNo){
            var item = new ItemSerial();
            string query = $@"SELECT 
            SNI.[Item No_], SNI.[Serial No_], BI.[Description]
            FROM [{_schema}$Serial No_ Information] SNI
            LEFT JOIN [{_schema}$Item] BI ON BI.No_ = SNI.[Item No_]
            WHERE [Serial No_] = @ItemCode";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@ItemCode", SerialNo);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if(reader.Read()){
                            item = new ItemSerial{
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                SerialNo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                            };
                        }
                    }
                }
            }
            
            return item;
        }
    }
}