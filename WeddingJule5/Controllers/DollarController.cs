using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    public class DollarController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        public ActionResult DollarIndex()
        {
            DollarViewModel dvm = new DollarViewModel();
            dvm.incomeDollars = db.Dollars.Where<Dollar>(d => d.price > 0);
            dvm.outcomeDollars = db.Dollars.Where<Dollar>(d => d.price < 0);
            decimal outcome = dvm.outcomeDollars.Sum<Dollar>(d => d.price);

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            System.Globalization.NumberFormatInfo currencyFormat = ci.NumberFormat;
            currencyFormat.CurrencyNegativePattern = 1;

            dvm.totalIncomePrice = dvm.incomeDollars.Sum<Dollar>(d => d.price).ToString("C", ci);
            dvm.totalOutcomePrice = dvm.outcomeDollars.Sum<Dollar>(d => d.price).ToString("C", ci);
            dvm.totalPrice = db.Dollars.Sum(d => d.price).ToString("C", ci);
            dvm.ci = ci;

            return View(dvm);
        }
    }
}