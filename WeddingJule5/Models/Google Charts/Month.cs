using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Models
{
    public class MonthVM
    {
        public SelectList Categories { get; set; }
        public int? categoryId { get; set; }
        public IEnumerable<Month> months { get; set; }
        public IEnumerable<string> categories { get; set; }
    }

    public class Month
    {
        public int? month { get; set; }
        public string monthName { get; set; }
        public IEnumerable<decimal> prices { get; set; }

        public const int Proportion = 100;
    }
}