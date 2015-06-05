using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using System.Data.Entity;

namespace WeddingJule.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        public ActionResult ListExpense(int PageNumber = 1, int? category = null, string filter = null)
        {
            IEnumerable<Expense> allExpenses = db.Expenses.Include(p => p.Category);

            int pageSize = 8; // количество объектов на страницу
            IQueryable<Expense> expenses = allExpenses.AsQueryable<Expense>();
            if (category != null && category != 0)
                expenses = expenses.Where(p => p.CategoryId == category);

            if (!string.IsNullOrWhiteSpace(filter))
                expenses = expenses.Where(p => p.name.Contains(filter));

            List<Category> categories = db.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { name = "Все", Id = 0 });

            IEnumerable<Expense> expensesPerPages = expenses.OrderByDescending(p => p.date).Skip((PageNumber - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = PageNumber, PageSize = pageSize, TotalItems = expenses.Count() };
            decimal? categoryExpenses = null;
            if (category.HasValue && category != 0 &&  expenses.Count()>0 )
                categoryExpenses = expenses.Sum(p => p.price);

            ExpenseViewModel plvm = new ExpenseViewModel
            {
                Expenses = expenses.ToList(),
                Categories = new SelectList(categories, "Id", "name"),
                category = category,
                PageInfo = pageInfo,
                PageExpenses = expensesPerPages,
                AllExpenses = allExpenses,
                CategoryExpenses = categoryExpenses,
                filter = filter
            };

            return View(plvm);
        }

        [HttpGet]
        public ActionResult Create(int? category)
        {
            SelectList categories = new SelectList(db.Categories, "Id", "name");
            CreateExpenseViewModel cevm = new CreateExpenseViewModel() { Categories = categories, categoryId = category };
            return PartialView(cevm);
        }

        [HttpPost]
        public ActionResult Create(CreateExpenseViewModel cevm)
        {
            if (ModelState.IsValid)
            {
                Expense expense = cevm.expense;
                expense.CategoryId = cevm.categoryId;
                db.Expenses.Add(expense);
                db.SaveChanges();
                return PartialView("Success");
             }
            else
            {
                SelectList categories = new SelectList(db.Categories, "Id", "name");
                cevm.Categories = categories;
                return PartialView(cevm);
            }
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
                CreateExpenseViewModel cevm = new CreateExpenseViewModel();
                cevm.expense = expense;
                cevm.categoryId = expense.CategoryId;
                SelectList categories = new SelectList(db.Categories, "Id", "name");
                cevm.Categories = categories;
                return PartialView(cevm);
            }

            return RedirectToAction("ListExpense", new { page = 1, category = expense.CategoryId });
        }

        [HttpPost]
        public ActionResult Edit(CreateExpenseViewModel cevm)
        {
            if (ModelState.IsValid)
            {
                Expense expense = cevm.expense;
                expense.CategoryId = cevm.categoryId;
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("Success");
            }

            SelectList categories = new SelectList(db.Categories, "Id", "name");
            cevm.Categories = categories;
            return PartialView(cevm);
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
                return PartialView(expense);

            return RedirectToAction("ListExpense", new { page = 1, category = expense.CategoryId });
        }

        [HttpPost]
        public ActionResult Delete(Expense expense)
        {
            int? categoryId = expense.CategoryId;
            db.Expenses.Attach(expense);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("ListExpense", new { page = 1, category = categoryId });
        }

        public ActionResult ExpenseDetails(int id)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense != null)
            {
                Category category = db.Categories.Find(expense.CategoryId);
                expense.Category = category;

                return PartialView("ExpenseDetails", expense);
            }
            return HttpNotFound();
        }

        public ActionResult Index()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }

            ViewBag.loginfo = result;

            return PartialView();
        }
    }
}