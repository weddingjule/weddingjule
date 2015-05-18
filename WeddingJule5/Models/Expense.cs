using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule5.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        [Display(Name = "Наименование траты")]
        public string name { get; set; }
        [Display(Name = "Стоимость")]
        public decimal price { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}   