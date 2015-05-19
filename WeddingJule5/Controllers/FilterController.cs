using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using System.Data.Entity;

namespace WeddingJule.Controllers
{
    public class FilterController : Controller
    {
        ExpenseContext db = new ExpenseContext();   

        public ActionResult Index(int? category)
        {
            IQueryable<Expense> expenses = db.Expenses.Include(p => p.Category);
            if (category != null && category != 0)
            {
                expenses = expenses.Where(p => p.CategoryId == category);
            }

            List<Category> categories = db.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { name = "Все", Id = 0 });

            ExpenseViewModel plvm = new ExpenseViewModel
            {
                Expenses = expenses.ToList(),
                Categories = new SelectList(categories, "Id", "name")
            };
            return View(plvm);
        }
    }
}