using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.Repository
{
    public class EFDbContext : DbContext
    {
        public DbSet<Expense> expenses;
        public DbSet<Expense> Expenses { get; set; }
    }
}