using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Models
{
    public class ExpenseViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public SelectList Categories { get; set; }
        public int? category { get; set; }

        public IEnumerable<Expense> PageExpenses { get; set; }
        public IEnumerable<Expense> AllExpenses { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}