using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Models
{
    public class ChartCategory
    {
        public IEnumerable<CharData> charDatas { get; set; }
        public SelectList months { get; set; }
        public int? month { get; set; }
    }

    public class CharData
    {
        public string name { get; set; }
        public decimal price { get; set; }
    }
}