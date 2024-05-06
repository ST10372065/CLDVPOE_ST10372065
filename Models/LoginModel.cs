using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class LoginModel
    {
        public static String con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_String);

        public int SelectUser(string email, string password)
        {
            int userId = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_String))
            {
                string sql = "SELECT userID FROM userTable WHERE userEmail = @Email AND userPassword = @Password";
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
                    // Log the exception or handle it appropriately
                    // For now, rethrow the exception
                    throw ex;
                }
            }
            return userId;
        }
    }
}