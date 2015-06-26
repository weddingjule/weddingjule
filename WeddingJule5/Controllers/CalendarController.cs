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
            IQueryable<Expense> expenses = db.Expenses.AsQueryable<Expense>();

            IEnumerable<DayInfo> days = from expense in expenses
                                        group expense by expense.date into m
                                        orderby m.Key
                                        select new DayInfo() { date = m.Key, price = m.Sum(e => e.price)};

            List<DayInfo> dayList = days.ToList<DayInfo>();

            foreach(DayInfo day in dayList)
            {
                IEnumerable<Expense> dayExpenses = expenses.Where<Expense>(e => e.date == day.date);
                day.setInfo(dayExpenses);
                string d = day.info;
            }

            return Json(dayList, JsonRequestBehavior.AllowGet);
        }
    }

    public class DayInfo
    {
        public DateTime date;
        public decimal price;

        public void setInfo(IEnumerable<Expense> expenses)
        {
            string expenseNameInfo = string.Empty;

            int count = expenses.Count();
            if (count == 1)
                expenseNameInfo = expenses.ElementAt<Expense>(0).name;
            else if (count > 1)
            {
                decimal maxprice = expenses.Max(e => e.price);
                Expense expense = expenses.Where(e => e.price == maxprice).Single();
                expenseNameInfo = expense.name;
            }

            info = date.Date.ToShortDateString() + " на " + price.ToString() + ": " + expenseNameInfo;
        }

        public string info;
    }
}