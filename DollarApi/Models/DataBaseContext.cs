using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeddingJule.Models;

namespace DollarApi.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Dollar> dollars { get; set; }
    }
}