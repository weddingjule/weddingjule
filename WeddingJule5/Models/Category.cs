using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingJule5.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string name { get; set; }

        public IEnumerable<Expense> expenses { get; set; }
    }
} 