using System.Data.SqlClient;
using System.Reflection;

namespace ST10390916_CLDV_POE.Models
{
    public class Product
    {

        public static string conString = "Server=tcp:cldvst10390916sql-server.database.windows.net,1433;Initial Catalog=cldvst10390916sql-DB;Persist Security Info=False;User ID=Weylin;Password=6!n5J$J7asn7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(conString);

        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public bool Availability { get; set; }
        public int OwnerID { get; set; }

        //-----------------------------------------Insert Product------------------------------------------------------------

        public int InsertProduct(Product product)
        {
            string sql = "INSERT INTO ProductTbl (product_name, product_price, product_category, availability, owner_id) VALUES (@Name, @Price, @Category, @Availability, @OwnerID)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            cmd.Parameters.AddWithValue("@Availability", product.Availability);
            cmd.Parameters.AddWithValue("@OwnerID", product.OwnerID);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }

        //----------------------------------------Select all products----------------------------------------------------------------------

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM ProductTbl";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product
                    {
                        Name = rdr["product_name"].ToString(),
                        Price = Double.Parse(rdr["product_price"].ToString()),
                        Category = rdr["product_category"].ToString(),
                        Availability = Boolean.Parse(rdr["availability"].ToString()),
                        OwnerID = int.Parse(rdr["owner_id"].ToString())
                    };

                    products.Add(product);
                }
            }

            return products;
        }


        public static List<Product> GetUserProducts(int? userID)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM ProductTbl WHERE owner_id = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product
                    {
                        Name = rdr["product_name"].ToString(),
                        Price = Double.Parse(rdr["product_price"].ToString()),
                        Category = rdr["product_category"].ToString(),
                        Availability = Boolean.Parse(rdr["availability"].ToString()),
                        OwnerID = int.Parse(rdr["owner_id"].ToString())
                    };

                    products.Add(product);
                }
            }

            return products;
        }

    }
}
