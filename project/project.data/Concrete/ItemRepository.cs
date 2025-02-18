using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class ItemRepository : IItemRepository{
        private readonly string _connectionString;
        private static string schema;
        public ItemRepository(IConfiguration configuration){
            _connectionString = configuration.GetConnectionString("Default");
            schema = "Bilgitas";
        }

        public DataResponse<List<Item>> GetAll(int currentPage, int itemPerPage){
            List<Item> items = new List<Item>();
            int offset = (currentPage - 1) * itemPerPage;
            string query = $@"SELECT BIC.[Description], BI.[Product Group Code], 
            BI.[No_], BI.[Description], BI.[No_ 2], 
            SP.[Currency Code], SP.[Unit Price]
            FROM [{schema}$Item] BI 
            INNER JOIN [Bilgitas$Item Category] BIC on 
            BIC.[Code] = BI.[Item Category Code]
            LEFT JOIN [Bilgitas$Sales Price] SP on
            SP.[Item No_] = BI.[No_]
            WHERE SP.[Sales Code] != 'LISTE' OR SP.[Sales Code] IS NULL
            ORDER BY BI.[No_]
            OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@Offset", offset);
                    cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
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

            if(items.Count == 0){
                return new DataResponse<List<Item>>{
                    Data = new List<Item>(),
                    Message = "Items failed",
                    Success = false
                };
            }

            return new DataResponse<List<Item>>{
                Data = items,
                Message = "Items successful",
                Success = true
            };
        }
        
        public DataResponse<List<ItemCategory>> GetItemCategoriesWithChild(string itemCode){
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            string query = $@"SELECT 
            [Item Category Code], [Description]
            FROM [{schema}$Product Group]
            WHERE [Item Category Code] = @itemCode";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@itemCode", itemCode);

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            ItemCategory item = new ItemCategory{
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if(itemCategories.Count == 0){
                return new DataResponse<List<ItemCategory>>{
                    Data = new List<ItemCategory>(),
                    Message = "Item Categories failed",
                    Success = false
                };
            }

            return new DataResponse<List<ItemCategory>>{
                Data = itemCategories,
                Message = "Item Categories successful",
                Success = true
            };
        }

        public DataResponse<List<ItemCategory>> GetItemCategories(){
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            string query = $@"SELECT 
            [Code], [Description]
            FROM [{schema}$Item Category];";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){
                    
                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        while(reader.Read()){
                            ItemCategory item = new ItemCategory{
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if(itemCategories.Count == 0){
                return new DataResponse<List<ItemCategory>>{
                    Data = new List<ItemCategory>(),
                    Message = "Item Categories failed",
                    Success = false
                };
            }

            return new DataResponse<List<ItemCategory>>{
                Data = itemCategories,
                Message = "Item Categories successful",
                Success = true
            };
        }

        public int GetCount(int itemPerPage){
            int count = 0;
            string query = $@"SELECT COUNT('TotalCount') AS 'TotalCount' FROM [{schema}$Item]";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if(reader.Read()){
                            count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        }
                    }
                }
            }

            int totalPage = (int)Math.Ceiling((double)count / itemPerPage);

            return totalPage;
        }

        public DataResponse<ItemDetail> GetItemDetail(){
            int count = 0;
            string query = $@"SELECT COUNT('TotalCount') AS 'TotalCount' FROM [{schema}$Item]";
            
            using(SqlConnection conn = new SqlConnection(_connectionString)){
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(query, conn)){

                    using(SqlDataReader reader = cmd.ExecuteReader()){
                        if(reader.Read()){
                            count = reader.IsDBNull(0) ? 0 :reader.GetInt32(0);
                        }
                    }
                }
            }
            
            return null;
        }
    }
}