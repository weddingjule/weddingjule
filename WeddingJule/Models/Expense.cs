using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        [Display(Name="Наимнование траты")]
        public string name{get; set;}
        [Display(Name="Стоимость")]
        public decimal price { get; set; }
    }
}