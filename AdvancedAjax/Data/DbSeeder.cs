
using AdvancedAjax.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedAjaxEFProject.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (!db.Products.Any())
            {
                db.Products.AddRange(new List<Product> {
                    new Product { Name = "Mobile A", Category = "electronics", Price = 600 },
                    new Product { Name = "Mobile B", Category = "electronics", Price = 850 },
                    new Product { Name = "TV", Category = "electronics", Price = 1500 },
                    new Product { Name = "Book", Category = "stationery", Price = 20 },
                });
            }

            if (!db.Users.Any())
            {
                var user = new User { Name = "Alice" };
                db.Users.Add(user);
                db.Orders.AddRange(new List<Order> {
                    new Order { Item = "Mobile A", User = user },
                    new Order { Item = "TV", User = user }
                });
            }

            db.SaveChanges();
        }
    }
}
