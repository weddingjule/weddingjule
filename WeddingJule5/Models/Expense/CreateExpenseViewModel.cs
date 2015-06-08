using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Models
{
    public class CreateExpenseViewModel
    {
        public SelectList Categories { get; set; }
        public int? categoryId { get; set; }
        public Expense expense{get; set;}
    }
}