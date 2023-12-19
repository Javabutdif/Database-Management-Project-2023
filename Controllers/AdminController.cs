using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Website_Database_New.Data;
using Website_Database_New.Models;

namespace Website_Database_New.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private CustomerData cus = new CustomerData();
        public  List<CustomerData> Datas = new List<CustomerData>();
        public  List<ItemData> dataItem = new List<ItemData>();
       

        public AdminController(ApplicationDbContext db )
        {
            _db = db;
          
        }
        public async Task<IActionResult> Customer()
        {
           

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM Customers WHERE c_status = 'TRUE'";
                _db.Database.OpenConnection();

                

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        CustomerData customerData = new CustomerData();
                        customerData.c_id = result.GetInt32(0);
                        customerData.c_name = result.GetString(1);
                        customerData.c_email = result.GetString(2);
                        customerData.c_address = result.GetString(3);
                        customerData.c_status = result.GetString(4);
                        customerData.c_password = result.GetString(5);

                        Datas.Add(customerData);
                    }
                }
            }
            return View(Datas);
            
        }
        public async Task<IActionResult> Items()
        {


            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM items";
                _db.Database.OpenConnection();



                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        ItemData item = new ItemData();
                        item.item_id = result.GetInt32(0);
                        item.item_isbn = result.GetInt32(1);
                        item.item_title = result.GetString(2);
                        item.item_author = result.GetString(3);
                        item.item_genre = result.GetString(4);
                        item.item_price = result.GetDecimal(5);
                        item.item_type = result.GetString(6);

                        dataItem.Add(item);
                    }
                }
            }
            return View(dataItem);
        }


        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateItem()
        {
            return View();
        }

        public IActionResult CreateItemAction(int isbn, string title, string author, string genre, int price, string type)
        {

            

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "INSERT INTO items " +
                                    " (item_isbn , item_title , item_author, item_genre," +
                                    "item_price, item_type ) VALUES " +
                                    "(@isbn , @title , @author , @genre ,@price ,@type) ";
                                    
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@isbn", isbn));
                command.Parameters.Add(new SqlParameter("@title", title));
                command.Parameters.Add(new SqlParameter("@author", author));
                command.Parameters.Add(new SqlParameter("@genre", genre));
                command.Parameters.Add(new SqlParameter("@price", price));
                command.Parameters.Add(new SqlParameter("@type", type));
                

                
                command.ExecuteNonQuery();

            }
           

            return RedirectToAction("Items", "Admin");
        }
        public async Task<IActionResult> CreateCustomer(string name, string email, string address, string password)
        {
          
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

            return RedirectToAction("Customer", "Admin");
        }
        
        public async Task<IActionResult> Edit(string id)
        {
           

            

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM Customers WHERE c_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));

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
            }
            ViewBag.id = cus.c_id;
            ViewBag.name = cus.c_name;
            ViewBag.email = cus.c_email;
            ViewBag.address = cus.c_address;
            ViewBag.password = cus.c_password;

            return View();

       

        }

        public async Task<IActionResult> EditItems(string id)
        {
            ItemData item = new ItemData();



            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM items WHERE item_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {

                        item.item_id = result.GetInt32(0);
                        item.item_isbn = result.GetInt32(1);
                        item.item_title = result.GetString(2);
                        item.item_author = result.GetString(3);
                        item.item_genre = result.GetString(4);
                        item.item_price = result.GetDecimal(5);
                        item.item_type = result.GetString(6);
                    }
                }
            }
            ViewBag.id = item.item_id;
            ViewBag.isbn = item.item_isbn;
            ViewBag.title = item.item_title;
            ViewBag.author = item.item_author;
            ViewBag.genre = item.item_genre;
            ViewBag.price = item.item_price;
            ViewBag.type = item.item_type;

            return View();



        }
        public IActionResult EditItemsAction(string id, int isbn, string title, string author, string genre, decimal price,string type)
        {

          

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "UPDATE items  SET" +
                                    " item_isbn = @isbn , item_title = @title , item_author = @author , item_genre = @genre," +
                                    "item_price = @price , item_type = @type  " +
                                    "WHERE item_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@isbn", isbn));
                command.Parameters.Add(new SqlParameter("@title", title));
                command.Parameters.Add(new SqlParameter("@author", author));
                command.Parameters.Add(new SqlParameter("@genre", genre));
                command.Parameters.Add(new SqlParameter("@price", price));
                command.Parameters.Add(new SqlParameter("@type", type));
                command.Parameters.Add(new SqlParameter("@id", id));

                command.ExecuteNonQuery();

            }

            return RedirectToAction("Items", "Admin");
        }
        public IActionResult EditCustomer(string id,string name, string email, string address, string password)
        {

            cus.c_name = name;
            cus.c_email = email;
            cus.c_address = address;
            cus.c_password = password;

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "UPDATE Customers  SET" +
                                    " c_name = @name , c_email = @email , c_address = @address , c_password = @password " +
                                    "WHERE c_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@name", cus.c_name));
                command.Parameters.Add(new SqlParameter("@email", cus.c_email));
                command.Parameters.Add(new SqlParameter("@address", cus.c_address));
                command.Parameters.Add(new SqlParameter("@password", cus.c_password));
                command.Parameters.Add(new SqlParameter("@id", id));

                command.ExecuteNonQuery();

            }

            return RedirectToAction("Customer", "Admin");
        }
        public IActionResult DeleteItems(string id)
        {



            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "DELETE FROM items WHERE item_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));


                command.ExecuteNonQuery();

            }

            return RedirectToAction("Items", "Admin");

        }

        public  IActionResult Delete(string id)
        {



            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "UPDATE Customers SET c_status = 'FALSE' WHERE c_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));


                command.ExecuteNonQuery();

            }

            return RedirectToAction("Customer", "Admin");

        }
    }
    }
