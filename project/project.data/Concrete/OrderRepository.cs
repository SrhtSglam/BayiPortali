using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        private readonly string _schema;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public ItemDetail GetItemDetail(string itemCode)
        {
            ItemDetail item = new ItemDetail();
            string query = @$"SELECT 
            ITE.[No_] AS ItemNo, 
            ITE.[No_ 2] AS ItemNo2, 
            ITE.[Description], 
            ITE.[Base Unit of Measure] AS BaseUnit, 
            ITE.[Aylık Baskı Hacmi] AS MonthlyPrintVolume, 
            ITE.[Baskı Kapasitesi (Num)] AS PrintingCapacity, 
            ITE.[Picture],
            IC.[Description] AS ItemCategoryDescription, 
            ITE.[Product Group Code] AS ProductGroup
            FROM [{_schema}$Item] ITE
            INNER JOIN [{_schema}$Item Category] IC on 
            IC.[Code] = ITE.[Item Category Code]
            WHERE [No_] = @itemCode";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemCode", itemCode);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                item = new ItemDetail
                                {
                                    // ItemNo = reader["No_"].ToString(),
                                    // ItemNo = reader.GetString(0), //? string.Empty : reader.GetString(0),
                                    ItemNo = reader.IsDBNull(reader.GetOrdinal("ItemNo")) ? string.Empty : reader["ItemNo"].ToString()!,
                                    ItemNo2 = reader.IsDBNull(reader.GetOrdinal("ItemNo2")) ? string.Empty : reader["ItemNo2"].ToString()!,
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString()!,
                                    BaseUnitOfMeasure = reader.IsDBNull(reader.GetOrdinal("BaseUnit")) ? string.Empty : reader["BaseUnit"].ToString()!,
                                    MonthlyPrintVolume = reader.IsDBNull(reader.GetOrdinal("MonthlyPrintVolume")) ? 0 : Convert.ToInt32(reader["MonthlyPrintVolume"]),
                                    PrintingCapacity = reader.IsDBNull(reader.GetOrdinal("PrintingCapacity")) ? 0 : Convert.ToInt32(reader["PrintingCapacity"]),
                                    ItemCategory = reader.IsDBNull(reader.GetOrdinal("ItemCategoryDescription")) ? string.Empty : reader["ItemCategoryDescription"].ToString()!,
                                    ProductGroup = reader.IsDBNull(reader.GetOrdinal("ProductGroup")) ? string.Empty : reader["ProductGroup"].ToString()!
                                };

                                if(item.MonthlyPrintVolume != 0 || item.PrintingCapacity != 0)
                                    item.Featured = true;

                                if (!reader.IsDBNull(6))
                                {
                                    byte[] pictureData = (byte[])reader[6];
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
            }
            catch
            {

            }

            return item;
        }

        public List<BasketItem> GetBasketByUserId(int ConfirmedStatus)
        {
            List<BasketItem> items = new List<BasketItem>();
            string query = @$"SELECT 
            DATEADD(mi, DATEDIFF(mi, GETUTCDATE(), GETDATE()), WB.[Time Stamp]), 
            WB.[Item No_], Item.[Description], WB.[Sales Description], WB.[Quantity], Item.[Base Unit of Measure]
            FROM [{_schema}$Web Basket] WB
            INNER JOIN [{_schema}$Item] Item
            ON WB.[Item No_] = Item.[No_]
            WHERE WB.[User ID] = @UserId AND WB.[Confirmed] = @ConfirmedStatus";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", WebLoginUser.AuthId);
                        cmd.Parameters.AddWithValue("@ConfirmedStatus", ConfirmedStatus);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    BasketItem item = new BasketItem
                                    {
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
            }
            catch
            {

            }

            return items;
        }

        public List<Item> GetAll(int currentPage, int itemPerPage, string selectedItemCode, string selectedSubItemCode)
        {
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
            ";

            if(!string.IsNullOrEmpty(selectedItemCode)){
                query += "AND BI.[Item Category Code] = @ItemCode";
            }

            if(!string.IsNullOrEmpty(selectedSubItemCode)){
                query += " AND BI.[SubCategory Code] = @SubItemCode";
            }

            query += "ORDER BY BI.[No_] OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Offset", offset);
                        cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);

                        if (!string.IsNullOrEmpty(selectedItemCode))
                            cmd.Parameters.AddWithValue("@ItemCode", selectedItemCode);

                        if (!string.IsNullOrEmpty(selectedSubItemCode))
                            cmd.Parameters.AddWithValue("@SubItemCode", selectedSubItemCode);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Item item = new Item
                                    {
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
            catch
            {

            }

            return items;
        }

        public int GetFilterItemCount(string selectedItemCode, string selectedSubItemCode)
        {
            int count = 0;

            string query = $@"
                SELECT COUNT(*)
                FROM [{_schema}$Item] BI 
                INNER JOIN [{_schema}$Item Category] BIC ON 
                    BIC.[Code] = BI.[Item Category Code]
                LEFT JOIN [{_schema}$Sales Price] SP ON
                    SP.[Item No_] = BI.[No_]
                WHERE SP.[Sales Code] = 'BAYI'";

            if (!string.IsNullOrEmpty(selectedItemCode))
            {
                query += " AND BI.[Item Category Code] = @ItemCode";
            }

            if (!string.IsNullOrEmpty(selectedSubItemCode))
            {
                query += " AND BI.[Product Group Code] = @SubItemCode";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters for the query
                        if (!string.IsNullOrEmpty(selectedItemCode))
                            cmd.Parameters.AddWithValue("@ItemCode", selectedItemCode);

                        if (!string.IsNullOrEmpty(selectedSubItemCode))
                            cmd.Parameters.AddWithValue("@SubItemCode", selectedSubItemCode);

                        count = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {

            }

            return count;
        }

        public List<Item> GetAll(int currentPage, int itemPerPage, string itemCode)
        {
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
            WHERE SP.[Sales Code] = 'BAYI' AND BI.[No_] LIKE '%' + @ItemCode + '%'
            ORDER BY BI.[No_]";
            // OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // cmd.Parameters.AddWithValue("@Offset", offset);
                        // cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);
                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Item item = new Item
                                    {
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
            catch
            {

            }

            return items;
        }

        public List<KeyValueItem> GetItemCategories()
        {
            List<KeyValueItem> itemCategories = new List<KeyValueItem>();
            string query = $@"SELECT 
            [Code], [Description]
            FROM [{_schema}$Item Category];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KeyValueItem item = new KeyValueItem
                            {
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if (itemCategories.Count == 0)
            {
                return new List<KeyValueItem>();
            }

            return itemCategories;
        }

        public List<KeyValueItem> GetItemCategories(string SelectedItemCode)
        {
            List<KeyValueItem> itemCategories = new List<KeyValueItem>();
            string query = $@"SELECT 
            [Item Category Code], [Description]
            FROM [{_schema}$Product Group]
            WHERE [Item Category Code] = @itemCode";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@itemCode", SelectedItemCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KeyValueItem item = new KeyValueItem
                            {
                                Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Description = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            };
                            itemCategories.Add(item);
                        }
                    }
                }
            }

            if (itemCategories.Count == 0)
            {
                return new List<KeyValueItem>();
            }

            return itemCategories;
        }

        public List<Item> GetItemsByCategory(int currentPage, int itemPerPage, string selectedItemCode, string selectedSubCategory)
        {
            List<Item> items = new List<Item>();
            int offset = (currentPage - 1) * itemPerPage;

            string query = $@"SELECT BIC.[Description], BI.[Product Group Code], 
                      BI.[No_], BI.[Description], BI.[No_ 2], 
                      SP.[Currency Code], SP.[Unit Price]
                      FROM [{_schema}$Item] BI 
                      INNER JOIN [{_schema}$Item Category] BIC on BIC.[Code] = BI.[Item Category Code]
                      LEFT JOIN [{_schema}$Sales Price] SP on SP.[Item No_] = BI.[No_]
                      WHERE BIC.[Code] = @SelectItem AND SP.[Sales Code] = 'BAYI'";

            if (!string.IsNullOrEmpty(selectedSubCategory))
            {
                query += " AND BI.[Product Group Code] = @ProductGroup";
            }

            query += " ORDER BY BI.[No_] OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Offset", offset);
                        cmd.Parameters.AddWithValue("@ItemPerPage", itemPerPage);
                        cmd.Parameters.AddWithValue("@SelectItem", selectedItemCode);

                        if (!string.IsNullOrEmpty(selectedSubCategory))
                        {
                            cmd.Parameters.AddWithValue("@ProductGroup", selectedSubCategory);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new Item
                                {
                                    ItemCategory = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                    ProductGroup = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                    ItemCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                    ItemDescription = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                    AlternativeCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                    CurrencyCode = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                    UnitPrice = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6)
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                // Log
            }

            return items;
        }


        public List<ItemComponent> GetComponentsByItemNo(string itemNo)
        {
            List<ItemComponent> items = new List<ItemComponent>();
            string query = $@"SELECT 
            IAC.[Item No_], IAC.[Component Item No_], IAC.[Description]
            FROM [{_schema}$Item Applicable Components] IAC
            WHERE [Item No_] = @itemNo";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemNo", itemNo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ItemComponent item = new ItemComponent
                                    {
                                        ItemNo = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                        ComponentItemNo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                        Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                    };
                                    items.Add(item);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch
            {

            }

            return items;
        }

        public async Task DeleteItemFromBasket(DateTime pDateTime, string pItemNo){
            string query = $@"DELETE FROM [Bilgitas$Web Basket]
            WHERE [User ID] = @UserId AND [Item No_] = @ItemNo AND [Time Stamp] = @DateTime AND [Confirmed] = 0";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", WebLoginUser.AuthId);
                        cmd.Parameters.AddWithValue("@ItemNo", pItemNo);
                        cmd.Parameters.AddWithValue("@DateTime", pDateTime.ToUniversalTime());
                        // cmd.Parameters.AddWithValue("@DateTime", pDateTime.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss.fff"));

                        await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine($"Ürün sepetten silindi: {pDateTime.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss.fff")}, {pItemNo}, {WebLoginUser.AuthId}");
                    }
                    await conn.CloseAsync();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}