using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class tester : Controller
    {
        public static String con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password={Teacupungold6};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_String);
        public IActionResult Index()
        {
            return View();
        }
    }
}
