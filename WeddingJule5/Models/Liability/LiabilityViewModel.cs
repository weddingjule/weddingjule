using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class LiabilityViewModel
    {
        public IEnumerable<Liability> liabilities { get; set; }
        public string totalPrice { get; set; }
    }
}