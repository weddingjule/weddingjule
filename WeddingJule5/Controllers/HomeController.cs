using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule5.Models;
using System.Data.Entity;

namespace WeddingJule5.Controllers
{
    public class HomeController : Controller
    {
        ExpenseContext db = new ExpenseContext();
         
        // Выводим всех футболистов
        public ActionResult Index()
        {
            var expenses = db.Expenses.Include(p => p.Category);
            //var expenses = db.Expenses;
            return View(expenses.ToList());
        }
    }
}