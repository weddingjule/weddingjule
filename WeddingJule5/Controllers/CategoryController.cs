using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    public class CategoryController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        public ActionResult Index()
        {
            IEnumerable<Category> categories = db.Categories.AsEnumerable<Category>();
            CategoryViewModel[] categoryViewModels = new CategoryViewModel[categories.Count<Category>()];
            for(int i=0; i<categories.Count<Category>(); i++)
            {
                Category category = categories.ElementAt<Category>(i);
                category.expenses = db.Expenses.Where<Expense>(e => e.CategoryId == category.Id);
                decimal totalCategoryPrice = category.expenses.Sum<Expense>(e=>e.price);
                string totalCategoryPriceString = string.Format("{0:N}", totalCategoryPrice);
                categoryViewModels[i] = new CategoryViewModel() { category = category, totalCategoryPriceString = totalCategoryPriceString };
            }

            return View(categoryViewModels.AsEnumerable<CategoryViewModel>());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return Create();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = db.Categories.Find(id);
            if (category != null)
            {
                return View(category);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return Edit(category.Id);
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = db.Categories.Find(id);

            if (category != null)
            {               
                List<Expense> expenses = db.Expenses.Where<Expense>(p => p.CategoryId == category.Id).ToList<Expense>();
                category.expenses = expenses;
                return View(category);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            List<Expense> expenses = db.Expenses.Where<Expense>(p=>p.CategoryId == category.Id).ToList<Expense>();
            foreach (Expense expense in expenses)
            {
                db.Expenses.Attach(expense);
                db.Expenses.Remove(expense);
            }

            db.Categories.Attach(category);
            db.Categories.Remove(category);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}