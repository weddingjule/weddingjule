using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class Target
    {
        public int? id { get; set; }
        public string name { get; set; }
        public bool done { get; set; }
        public decimal priority { get; set; }
    }
}