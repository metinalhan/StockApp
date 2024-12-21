using StockApp.Entities.Models;
using System.Data.Entity;

namespace StockApp.DAL.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext() : base("name=DefaultConnection") { }

        public DbSet<Product> Products { get; set; }
    }
}
