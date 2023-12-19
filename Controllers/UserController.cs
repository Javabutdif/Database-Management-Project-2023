using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Website_Database_New.Data;
using Website_Database_New.Models;

namespace Website_Database_New.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public List<DisplayObject> Datas = new List<DisplayObject>();
        public List<ItemData> dataItem = new List<ItemData>();

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Cart(int id)
        {
            ViewBag.id = id;
           

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Cart_Item.cart_item_id, item_title, item_type, quantity, total FROM items,Customers JOIN Cart_Item on Cart_Item.cart_id = Customers.c_id WHERE items.item_id = Cart_Item.item_id AND Customers.c_id = @id  ; ";
                _db.Database.OpenConnection();
                command.Parameters.Add(new SqlParameter("@id", id));



                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        DisplayObject obj = new DisplayObject();
                        obj.id = result.GetInt32(0);
                        obj.title = result.GetString(1);
                        obj.type = result.GetString(2);
                        obj.quantity = result.GetInt32(3);
                        obj.total =  result.GetInt32(4);
                        
                        Datas.Add(obj);
                    }
                }
            }
            return View(Datas);

         
        }
        public IActionResult Remove(int itemId , int id)
        {
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "DELETE FROM Cart_Item WHERE cart_item_id = @id; ";
                _db.Database.OpenConnection();
                command.Parameters.Add(new SqlParameter("@id", itemId));



                command.ExecuteNonQuery();
            }
            return RedirectToAction("Cart" , "User" , new {id = id});
        }
        public async Task<IActionResult> Shop(int id)
        {
            ViewBag.id = id;



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
      
        public List<Cart_Item> checkoutDisplay = new List<Cart_Item>();
        public async Task<IActionResult> Checkout(string id)
        {
            Cart_Item cart = new Cart_Item();
            int total = 0;
            ViewBag.id = id;


            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Cart_Item.cart_item_id, item_title, item_type, quantity, total FROM items,Customers JOIN Cart_Item on Cart_Item.cart_id = Customers.c_id WHERE items.item_id = Cart_Item.item_id AND Customers.c_id = @id  ; ";
                _db.Database.OpenConnection();
                command.Parameters.Add(new SqlParameter("@id", id));



                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        DisplayObject obj = new DisplayObject();
                        obj.id = result.GetInt32(0);
                        obj.title = result.GetString(1);
                        obj.type = result.GetString(2);
                        obj.quantity = result.GetInt32(3);
                        obj.total = result.GetInt32(4);

                        Datas.Add(obj);
                        total += result.GetInt32(4);
                    }
                }
            }
            ViewBag.total = total;
            return View(Datas);

        }
        public async Task<IActionResult> AddCart(int id, int itemId)
        {
            CustomerData cus = new CustomerData();
            CartData cart = new CartData();
            ItemData item = new ItemData();

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT c_name, item_title,item_type, item_isbn, item_price  FROM Customers, items , Cart WHERE c_id = @id AND item_id = @itemId";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@itemId", itemId));

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {

                       
                        cus.c_name = result.GetString(0);
                        item.item_title = result.GetString(1);
                        item.item_type = result.GetString(2);
                        item.item_isbn = result.GetInt32(3);
                        item.item_price = result.GetDecimal(4);
                       
                    }
                }
            }
       
            ViewBag.name = cus.c_name;
            ViewBag.title = item.item_title;
            ViewBag.type = item.item_type;
            ViewBag.isbn = item.item_isbn;
            ViewBag.price = item.item_price;
            ViewBag.id = id;
            ViewBag.itemId = itemId;
                return View();
        }

        public IActionResult AddCartAction(int id, int quantity , int itemid, int price)
        {
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "INSERT INTO Cart_Item" +
                                "(cart_id, item_id,quantity,total) VALUES " +
                                "(@cartId , @itemId, @qty, @tot);";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@cartId", id));
                command.Parameters.Add(new SqlParameter("@itemId", itemid));
                command.Parameters.Add(new SqlParameter("@qty", quantity));
                command.Parameters.Add(new SqlParameter("@tot", (price*quantity)));
        

                command.ExecuteNonQuery();

            }

            return RedirectToAction("Shop", "User", new { id = id });
        }
       
        public async Task<IActionResult> Order(int id , int total)
        {
            CustomerData cus = new CustomerData();
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

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "INSERT INTO Orders " +
                                        "(order_date , order_total, order_add , c_id) VALUES" +
                                        "( @odate,@total,@add, @id);";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@add", cus.c_address));
                command.Parameters.Add(new SqlParameter("@odate", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@total", total));

                command.ExecuteNonQuery();
               
            }

           
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "DELETE FROM Cart_Item WHERE cart_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));
                

                command.ExecuteNonQuery();

            }
            return RedirectToAction("Cart", "User", new { id = id });
        }
        public List<OrderData> orderData = new List<OrderData>();

        public async Task<IActionResult> Status(int id)
        {
            ViewBag.id = id;

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM Orders WHERE c_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        OrderData order = new OrderData();

                        order.order_id = result.GetInt32(0);
                        order.order_date = result.GetDateTime(1);
                        order.order_total = result.GetDecimal(2);
                        order.order_add = result.GetString(3);
               

                        orderData.Add(order);
                        
                    }
                }
            }
            return View(orderData);

        }

        public async Task<IActionResult> ViewData(int id)
        {
            ViewBag.id = id;

            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM Orders WHERE c_id = @id";
                _db.Database.OpenConnection();

                command.Parameters.Add(new SqlParameter("@id", id));

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        OrderData order = new OrderData();

                        order.order_id = result.GetInt32(0);
                        order.order_date = result.GetDateTime(1);
                        order.order_total = result.GetDecimal(2);
                        order.order_add = result.GetString(3);
                        order.order_status = result.GetString(4);

                        orderData.Add(order);

                    }
                }
            }
            return View(orderData);
        }

    }
}
