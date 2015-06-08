using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class DollarViewModel
    {
        public IEnumerable<Dollar> incomeDollars { get; set; }
        public string totalIncomePrice { get; set; }
        public IEnumerable<Dollar> outcomeDollars { get; set; }
        public string totalOutcomePrice { get; set; }
        public string totalPrice { get; set; }
        public CultureInfo ci { get; set; }
    }
}