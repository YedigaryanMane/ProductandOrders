using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp83
{
    interface DatabaseHelper<Type>
    {
        void Add(Type type);
        void Delete(int id);
        void Update(int id, Type type);
        List<Type> GetAll();
        Type Get(int id);

    }
    abstract class Product
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract decimal Price { get; set; }


        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public Product() { }
    }


    class ProductCollection : Product
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override decimal Price { get; set; }

        public const string CONNECTION_STRING = "Data Source=.;Initial Catalog=StoreeDb;Integrated Security=True;Encrypt=False";

        public ProductCollection(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public ProductCollection() { }

        public void Add(Product product)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "insert into Productt values(@Id,@Name,@Price )";
                    command.Parameters.Add(new SqlParameter("@Id", product.Id));
                    command.Parameters.Add(new SqlParameter("@Name", product.Name));
                    command.Parameters.Add(new SqlParameter("@Price", product.Price));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "delete from Productt where id = @Id";
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    command.ExecuteNonQuery();
                }
            }
        }

        public Product Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                Product product = new ProductCollection();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Productt where id = @Id";
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.Id = int.Parse(reader["@Id"].ToString());
                            product.Name = reader["@Name"].ToString();
                            product.Price = decimal.Parse(reader["@Price"].ToString());
                        }
                    }
                }
                return product;
            }
        }

        public List<Product> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                List<Product> list = new List<Product>();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Productt ";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new ProductCollection();

                            product.Id = int.Parse(reader["@Id"].ToString());
                            product.Name = reader["@Name"].ToString();
                            product.Price = decimal.Parse(reader["@Price"].ToString());
                            list.Add(product);
                        }
                    }
                }
                return list;
            }
        }

        public void Update(Product product)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "update Productt set Id = @Id,Name = @Name, Price = @Price )";
                    command.Parameters.Add(new SqlParameter("@Id", product.Id));
                    command.Parameters.Add(new SqlParameter("@Name", product.Name));
                    command.Parameters.Add(new SqlParameter("@Price", product.Price));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
    public delegate void OrderCompletedEventHandler();
    class Electronics : Product
    {
        public override int Id { get; set; }

        public override string Name { get; set; }

        public override decimal Price { get; set; }
    }

    class Clothing : Product
    {
        public override int Id { get; set; }

        public override string Name { get; set; }

        public override decimal Price { get; set; }
    }

    class Order
    {
        public event OrderCompletedEventHandler OrderCompleted;
        public int OrderID { get; set; }
        public String Product { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }

        public const string CONNECTION_STRING = "Data Source=.;Initial Catalog=StoreeDb;Integrated Security=True;Encrypt=False";

        public Order(int orderID, string product, int quantity, int totalAmount)
        {
            OrderID = orderID;
            Product = product;
            Quantity = quantity;
            TotalAmount = totalAmount;
        }
        public Order()
        { }

        public void Add(Order order)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "insert into Orders values(@OrderId,@Product,@Quantity,@TotalAmount)";
                    command.Parameters.Add(new SqlParameter("@OrderId", order.OrderID));
                    command.Parameters.Add(new SqlParameter("@Product", order.Product));
                    command.Parameters.Add(new SqlParameter("@Quantity", order.Quantity));
                    command.Parameters.Add(new SqlParameter("@TotalAmount", order.TotalAmount));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "delete from Orders where id = @OrderId";
                    command.Parameters.Add(new SqlParameter("@OrderId", id));

                    command.ExecuteNonQuery();
                }
            }
        }

        public Order Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                Order order = new Order();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Productt where id = @OrderId";
                    command.Parameters.Add(new SqlParameter("OrderId", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            order.OrderID = int.Parse(reader["@OrderId"].ToString());
                            order.Product = reader["@Productt"].ToString();
                            order.Quantity = int.Parse(reader["@Quantity"].ToString());
                            order.TotalAmount = int.Parse(reader["@TotalAmount"].ToString());
                        }
                    }
                }
                return order;
            }
        }

        public List<Order> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                List<Order> list = new List<Order>();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Orders ";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            order.OrderID = int.Parse(reader["@OrderId"].ToString());
                            order.Product = reader["@Name"].ToString();
                            order.Quantity = int.Parse(reader["@Quantity"].ToString());
                            list.Add(order);
                        }
                    }
                }
                return list;
            }
        }

        public void Update(Order order)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "update Orders set OrderID = @OrderId,Product = @Product,Quantity = @Quantity,TotalAmount = @TotalAmount )";

                    command.Parameters.Add(new SqlParameter("@OrderId", order.OrderID));
                    command.Parameters.Add(new SqlParameter("@Product", order.Product));
                    command.Parameters.Add(new SqlParameter("@TotalAmount", order.TotalAmount));
                    command.Parameters.Add(new SqlParameter("@Quantity", order.Quantity));

                    command.ExecuteNonQuery();
                }
            }
        }
    }

    class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    interface IProduct
    {
        void CalculateDiscount();
        Product GetProductDetails();
    }

    class Repository<T> : Product
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override decimal Price { get; set; }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            ProductCollection products = new ProductCollection();
            //products.Add(new ProductCollection(1, "Phone", 100000));
            //products.Add(new ProductCollection(2, "Computer", 200000));
            products.Delete(2);

        }
    }
   
}
