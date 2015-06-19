using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeddingJule.Models.Account;
using WeddingJule.Models;
using WeddingJule.Models.Map;

namespace WeddingJule.Models
{
    public class ExpenseContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Liability> Liabilities { get; set; }
        public DbSet<Dollar> Dollars { get; set; }
        public DbSet<Place> Places { get; set; }
    }
}