using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    public class CalendarController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: Calendar
        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult GetData()
        {
            IEnumerable<DayInfo> days = from expense in db.Expenses
                                        group expense by expense.date into m
                                        orderby m.Key
                         select new DayInfo () { date = m.Key, price = m.Sum(e=>e.price) };

            return Json(days, JsonRequestBehavior.AllowGet);
        }
    }

    public class DayInfo
    {
        public DateTime date;
        public decimal price;
    }
}