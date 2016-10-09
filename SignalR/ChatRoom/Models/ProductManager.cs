using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatRoom.Models
{
    public class ProductManager
    {
        public static List<Product> Products { get; set; }

        static ProductManager()
        {
            Products = new List<Product>();
            User toan = UserManager.Users.SingleOrDefault(a => string.Equals(a.UserName, "toan", StringComparison.OrdinalIgnoreCase));
            User kaka = UserManager.Users.SingleOrDefault(a => string.Equals(a.UserName, "kaka", StringComparison.OrdinalIgnoreCase));
            Products.Add(new Product { ProductId = 1, ProductName = "Milk", Active = true, Owner = toan });
            Products.Add(new Product { ProductId = 1, ProductName = "Ball", Active = false, Owner = toan });
            Products.Add(new Product { ProductId = 1, ProductName = "Meat", Active = true, Owner = kaka });
            Products.Add(new Product { ProductId = 1, ProductName = "Bread", Active = false, Owner = kaka });
        }
    }
}