using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    public class AutocompleteSearchController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: AutocompleteSearch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var expenses = db.Expenses.Where(a => a.name.Contains(term))
                            .Select(a => new { value = a.name })
                            .Distinct();

            return Json(expenses, JsonRequestBehavior.AllowGet);
        }
    }
}