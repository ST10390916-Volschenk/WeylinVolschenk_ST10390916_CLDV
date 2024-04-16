using System.Data.SqlClient;

namespace ST10390916_CLDV_POE.Models
{
    public class LoginModel
    {
        public static string conString = "Server=tcp:cldvst10390916sql-server.database.windows.net,1433;Initial Catalog=cldvst10390916sql-DB;Persist Security Info=False;User ID=Weylin;Password=6!n5J$J7asn7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public int SelectUser(string email, string password)
        {
            int userId = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(conString))
            {
                string sql = "SELECT user_id FROM Usertbl WHERE user_email = @Email AND user_password = @Password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return userId;
        }
    }
}
