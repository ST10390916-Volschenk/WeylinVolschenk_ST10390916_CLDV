using System.Data.SqlClient;

namespace ST10390916_CLDV_POE.Models
{
    public class Order
    {
        public static string conString = "Server=tcp:cldvst10390916sql-server.database.windows.net,1433;Initial Catalog=cldvst10390916sql-DB;Persist Security Info=False;User ID=Weylin;Password=6!n5J$J7asn7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(conString);

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ClientID { get; set; }
        public DateTime OrderDate { get; set; }
        public bool OrderProcessed { get; set; }

        //-----------------------------------------Insert new order------------------------------------------------------------------

        public int InsertOrder(Order order)
        {
            string sql = "INSERT INTO OrderTbl (product_id, client_id, order_date, order_processed) VALUES (@ProductID, @ClientID, @OrderDate, @OrderProcessed)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductID", order.ProductID);
            cmd.Parameters.AddWithValue("@ClientID", order.ClientID);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("@OrderProcessed", order.OrderProcessed);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }

        //---------------------------------------Get Order History------------------------------------------------------------

        public static List<Order> GetUserOrders(int userID)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM OrderTbl WHERE client_id = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order
                    {
                        OrderID = int.Parse(rdr["order_id"].ToString()),
                        ProductID = int.Parse(rdr["product_id"].ToString()),
                        ClientID = int.Parse(rdr["client_id"].ToString()),
                        OrderDate = (DateTime)rdr["order_date"],
                        OrderProcessed = Boolean.Parse(rdr["order_processed"].ToString())
                    };

                    orders.Add(order);
                }
            }

            return orders;
        }

        //---------------------------------------------------Select order product------------------------------------

        public static Product GetProduct(int productID)
        {
            Product product;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM ProductTbl WHERE product_id = @ProductID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", productID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                product = new Product
                {
                    Name = rdr["product_name"].ToString(),
                    Price = Double.Parse(rdr["product_price"].ToString()),
                    Category = rdr["product_category"].ToString(),
                    Availability = Boolean.Parse(rdr["availability"].ToString()),
                    OwnerID = int.Parse(rdr["owner_id"].ToString())
                };
            }
            return product;
        }

        //-----------------------------------------------Get sales history---------------------------------------

        public static List<Order> GetUserSales(int userID)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM OrderTbl o JOIN ProductTBL p ON o.product_id = p.product_id WHERE p.owner_id = @UserID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order
                    {
                        OrderID = int.Parse(rdr["order_id"].ToString()),
                        ProductID = int.Parse(rdr["product_id"].ToString()),
                        ClientID = int.Parse(rdr["client_id"].ToString()),
                        OrderDate = (DateTime)rdr["order_date"],
                        OrderProcessed = Boolean.Parse(rdr["order_processed"].ToString())
                    };

                    orders.Add(order);
                }
            }

            return orders;
        }

        //----------------------------------------Get client info----------------------------------------------------

        public static User GetUserInfo(int userID)
        {
            User user;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT * FROM UserTbl WHERE user_id = @ClientID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ClientID", userID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                user = new User
                {
                    Name = rdr["user_name"].ToString(),
                    Surname = rdr["user_surname"].ToString(),
                    Email = rdr["user_email"].ToString(),
                    Password = rdr["user_password"].ToString()

                };
            }
            return user;
        }

        //-----------------------------------------Process order------------------------------------------------------------------

        public int ProcessOrder(int orderID)
        {
            string sql = "Update OrderTbl SET order_processed = 1 WHERE order_id = @OrderID";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }
    }
}
