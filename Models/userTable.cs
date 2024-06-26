﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class userTable
    {
        public static String con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_String);

        public string Name { get; set; }

        public  string Surname { get; set; }

        public  string Email { get; set; }

        public  string Password { get; set; }

        /// <summary>
        /// inserts a new user into the database
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int insert_User(userTable m)
        {

            try
            {
                string sql = "INSERT INTO userTable (userName, userSurname, userEmail, userPassword) VALUES (@Name, @Surname, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Surname", m.Surname);
                cmd.Parameters.AddWithValue("@Email", m.Email);
                cmd.Parameters.AddWithValue("@Password", m.Password);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }

        }

    }
}
