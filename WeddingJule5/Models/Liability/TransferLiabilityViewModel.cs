using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Models.Liability
{
    public class TransferLiabilityViewModel
    {
        public SelectList Categories { get; set; }
        public Expense expense { get; set; }
        public int LiabilityID { get; set; }
    }
}