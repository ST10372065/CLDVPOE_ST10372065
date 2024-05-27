using Microsoft.AspNetCore.Mvc;
using ST10372065.Models;

namespace ST10372065.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }
        /// <summary>
        /// ensures that the user exists in the database.
        /// Provided in assignment
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var loginModel = new LoginModel();
            int userID = loginModel.SelectUser(email, password);
            if (userID != -1)
            {
                // Store userID in session
                HttpContext.Session.SetString("UserID", userID.ToString());

                // User found, proceed with login logic (e.g., set authentication cookie)
                // For demonstration, redirecting to a dummy page
                return RedirectToAction("MyWork", "Home", new { userID = userID });
            }
            else
            {
                // User not found, handle accordingly (e.g., show error message)
                return View("LoginFailed");
            }
        }
    }
}
