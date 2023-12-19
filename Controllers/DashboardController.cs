using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website_Database_New.Data;
using Website_Database_New.Models;

namespace Website_Database_New.Controllers
{
    public class DashboardController : Controller
    {
      
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult UserDashboard(int id, string name)
        {
            ViewBag.id = id;
            ViewBag.name = name;

            return View();
        }
      
        
            private readonly ApplicationDbContext _db;

            public DashboardController(ApplicationDbContext db)
            {
                _db = db;
            }
        [HttpPost]
        public async Task<IActionResult> YourAction(string email, string password)
        {
            CustomerData cus = new CustomerData();
            CartData cart = new CartData();
            if (email == "admin@gmail.com" && password == "admin")
            {
                return RedirectToAction("AdminDashboard", "Dashboard");
            }
            else
            {
                using (var command = _db.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Customers WHERE c_email = @CusEmail AND c_password = @CusPass AND c_status = 'TRUE'";
                    _db.Database.OpenConnection();

                    command.Parameters.Add(new SqlParameter("@CusEmail", email));
                    command.Parameters.Add(new SqlParameter("@CusPass", password));

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                           
                            cus.c_id = result.GetInt32(0);
                         
                         
                        }
                    }

                }


                if (cus.c_id == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                

                    return RedirectToAction("UserDashboard", "Dashboard", new { id = cus.c_id });
                }
            }
        }
        



    }
}
