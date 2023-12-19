using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Website_Database_New.Data;
using Website_Database_New.Models;

namespace Website_Database_New.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> CreateCustomer(string name, string email, string address, string password)
        {
            CustomerData cus = new CustomerData();
            cus.c_name = name;
            cus.c_email = email;
            cus.c_address = address;
            cus.c_password = password;

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "INSERT INTO Customers" +
                                "(c_name, c_email,c_address, c_status , c_password ) VALUES " +
                                "(@name , @email, @address, 'TRUE' , @pass);";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@name", cus.c_name));
                command.Parameters.Add(new SqlParameter("@email", cus.c_email));
                command.Parameters.Add(new SqlParameter("@address", cus.c_address));
                command.Parameters.Add(new SqlParameter("@pass", cus.c_password));

                command.ExecuteNonQuery();

            }
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM Customers WHERE c_email = @email";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@email", email));

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {

                        cus.c_id = result.GetInt32(0);
                        cus.c_name = result.GetString(1);
                        cus.c_email = result.GetString(2);
                        cus.c_address = result.GetString(3);
                        cus.c_status = result.GetString(4);
                        cus.c_password = result.GetString(5);
                    }
                }
                command.CommandText = "INSERT INTO Cart " +
                                        "(cart_id , date_created) VALUES " +
                                        "(@id , @date);";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", cus.c_id));
                command.Parameters.Add(new SqlParameter("@date", DateTime.Now));

                command.ExecuteNonQuery();
            }

            return RedirectToAction("Login", "Home");
        }
    }
  

}