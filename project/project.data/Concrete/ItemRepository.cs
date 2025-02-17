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
            string query = $@"SELECT 
            BIC.[Description], BI.[Product Group Code], BI.[No_], BI.[Description], [No_ 2], BI.[Item Transfer Type]
            FROM [{schema}$Item] BI 
            LEFT JOIN [{schema}$Item Category] BIC on BIC.[Code] = BI.[Item Category Code]
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
                                ItemCategory = reader.GetString(0),
                                ProductGroup = reader.GetString(1),
                                ItemCode = reader.GetString(2),
                                ItemDescription = reader.GetString(3),
                                AlternativeCode = reader.GetString(4),
                                StockTransferType = reader.GetInt32(5),
                                // StockTransferType = reader["Password"].ToString()
                                // SalesPrice = Convert.ToInt32(reader["Web Visibility"])
                                // StockAmount = Convert.ToInt32(reader["Web Visibility"])
                            };
                            items.Add(item);
                        }
                    }
                }
            }

            if(items.Count == 0){
                return new DataResponse<List<Item>>{
                    Data = null,
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
                                Code = reader.GetString(0),
                                Description = reader.GetString(1),
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
                                Code = reader.GetString(0),
                                Description = reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if(itemCategories.Count == 0){
                return new DataResponse<List<ItemCategory>>{
                    Data = null,
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
                            count = reader.GetInt32(0);
                        }
                    }
                }
            }

            int totalPage = (int)Math.Ceiling((double)count / itemPerPage);

            return totalPage;
        }
    }
}