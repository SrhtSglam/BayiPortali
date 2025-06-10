using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using project.data.Abstract;
using project.entity;

namespace project.data.Concrete{
    public class OtherRepository : IOtherRepository
    {
        private readonly string _connectionString;
        private readonly string _schema;
        public OtherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _schema = WebLoginUser.Company;
        }

        public int GetCount(string table_name)
        {
            int count = 0;
            string query = $@"SELECT COUNT(*) AS 'TotalCount' FROM [{_schema}${table_name}]";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        }
                    }
                }
            }

            return count;
        }

        public int GetCountPerPage(string table_name, int perPage)
        {
            int count = 0;
            string query = $@"SELECT COUNT(*) AS 'TotalCount' FROM [{_schema}${table_name}]";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        }
                    }
                }
            }

            int totalPage = (int)Math.Ceiling((double)count / perPage);

            return totalPage;
        }
    }
}