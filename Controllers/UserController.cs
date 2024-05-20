using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ST10372065.Models;

namespace ST10372065.Controllers
{
    public class UserController : Controller
    {
        private readonly userTable userTable;

        public UserController()
        {
            userTable = new userTable();
        }

        [HttpPost]
        public ActionResult SignUp(userTable user)
        {
            // Insert user into the database
            int result = userTable.insert_User(user);

            if (result > 0)
            {
                // User successfully signed up
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // Failed to sign up
                TempData["ErrorMessage"] = "Failed to sign up. Please try again.";
                return RedirectToAction("SignUp", "Home");
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }


    }
}