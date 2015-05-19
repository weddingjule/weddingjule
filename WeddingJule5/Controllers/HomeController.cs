using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using System.Data.Entity;

namespace WeddingJule.Controllers
{
    public class HomeController : Controller
    {
        ExpenseContext db = new ExpenseContext();         

        public ActionResult Index(int page = 1, int? category = null)
        {
            IEnumerable<Expense> allExpenses = db.Expenses.Include(p => p.Category);

            int pageSize = 10; // количество объектов на страницу
            IQueryable<Expense> expenses = allExpenses.AsQueryable<Expense>();
            if (category != null && category != 0)
                expenses = expenses.Where(p => p.CategoryId == category);

            List<Category> categories = db.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { name = "Все", Id = 0 });

            IEnumerable<Expense> expensesPerPages = expenses.OrderBy(p => p.date).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = expenses.Count() };

            ExpenseViewModel plvm = new ExpenseViewModel
            {
                Expenses = expenses.ToList(),
                Categories = new SelectList(categories, "Id", "name"),
                PageInfo = pageInfo,
                PageExpenses = expensesPerPages,
                AllExpenses = allExpenses
            };

            return View(plvm);
        }

        public ActionResult ListCategories(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            category.expenses = db.Expenses.Where(m => m.CategoryId == category.Id);
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "name");
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            db.Expenses.Add(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Expense expense = db.Expenses.Find(id);
            if (expense != null)
            {
                SelectList categories = new SelectList(db.Categories, "Id", "name");
                ViewBag.Categories = categories;
                return View(expense);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            db.Entry(expense).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Expense expense = db.Expenses.Find(id);
            Category category = db.Categories.Find(expense.CategoryId);
            expense.Category = category;

            if (expense != null)
                return View(expense);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Expense expense)
        {
            db.Expenses.Attach(expense);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}