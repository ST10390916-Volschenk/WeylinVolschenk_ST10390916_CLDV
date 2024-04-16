using System.Data.SqlClient;

namespace ST10390916_CLDV_POE.Models
{
    public class SignUpModel
    {
        public static string conString = "Server=tcp:cldvst10390916sql-server.database.windows.net,1433;Initial Catalog=cldvst10390916sql-DB;Persist Security Info=False;User ID=Weylin;Password=6!n5J$J7asn7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(conString);

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public int insert_User(SignUpModel m)
        {
            string sql = "INSERT INTO Usertbl (user_name, user_surname, user_email, user_password) VALUES (@Name, @Surname, @Email, @Password)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", m.Name);
            cmd.Parameters.AddWithValue("@Surname", m.Surname);
            cmd.Parameters.AddWithValue("@Email", m.Email);
            cmd.Parameters.AddWithValue("@Password", m.Password);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }







    }
}
