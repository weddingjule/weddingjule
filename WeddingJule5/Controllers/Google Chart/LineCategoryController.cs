using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    [Authorize]
    public class LineCategoryController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        public ActionResult LineCategory(int? categoryId)
        {
            if (categoryId != null && categoryId != 0)
                return LineCategoryValue(categoryId.Value);

            List<Expense> allExpenses = db.Expenses.ToList<Expense>();
            List<Category> categories = getCategories();
            string[] categoryNames = new string[categories.Count];

            IEnumerable<Month> months = from expense in allExpenses
                                        group expense by new { month = expense.date.Month } into m
                                        orderby m.Key.month
                                        select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            Month[] listMonths = new Month[months.Count()];

            for (int j = 0; j < months.Count(); j++)
            {
                Month month = months.ElementAt(j);
                decimal[] prices = new decimal[categories.Count];
                for (int i = 0; i < prices.Length; i++)
                {
                    Category category = categories.ElementAt(i);

                    categoryNames[i] = category.name;
                    IEnumerable<Expense> monthExpenses = category.expenses.Where(e => e.CategoryId == category.Id && e.date.Month == month.month);
                    decimal price = monthExpenses.Sum(e => e.price);
                    prices[i] = price;
                }

                IEnumerable<decimal> priceEnumerable = prices.AsEnumerable<decimal>();
                month.prices = priceEnumerable;
                listMonths[j] = month;
            }
            
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { name = "Суммарные траты", Id = -1 });
            categories.Insert(0, new Category { name = "Все категории", Id = 0 });

            MonthVM monthVM = new MonthVM()
            {
                months = listMonths,
                categories = categoryNames,
                Categories = new SelectList(categories, "Id", "name"),
                categoryId = 0,
            };

            return View("LineCategory", monthVM);
        }

        private List<Category> getCategories()
        {
            List<Category> categories = db.Categories.ToList<Category>();

            for (int i = 0; i < categories.Count; i++)
            {
                Category category = categories[i];
                category.expenses = db.Expenses.Where<Expense>(e => e.CategoryId == category.Id);
            }

            categories = categories.OrderByDescending(c => c.expenses.Sum(e => e.price)).ToList<Category>();

            return categories;
        }

        public ActionResult LineCategoryValue(int categoryId)
        {
            if (categoryId == -1)
                return LineCategorySummary();

            List<Expense> allExpenses = db.Expenses.Where(e => e.CategoryId == categoryId).ToList<Expense>();
            Category category = db.Categories.Find(categoryId);
            string[] categoryNames = { category.name };

            IEnumerable<Month> months = from expense in allExpenses
                                        group expense by new { month = expense.date.Month } into m
                                        orderby m.Key.month
                                        select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            Month[] listMonths = new Month[months.Count()];

            for (int j = 0; j < months.Count(); j++)
            {
                Month month = months.ElementAt(j);
                decimal[] prices = new decimal[1];
                decimal price = allExpenses.Where(e => e.CategoryId == category.Id && e.date.Month == month.month).Sum(e => e.price);
                prices[0] = price;

                month.prices = prices;
                listMonths[j] = month;
            }

            List<Category> categories = getCategories();

            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { name = "Суммарные траты", Id = -1 });
            categories.Insert(0, new Category { name = "Все категории", Id = 0 });

            MonthVM monthVM = new MonthVM()
            {
                months = listMonths,
                categories = categoryNames,
                Categories = new SelectList(categories, "Id", "name"),
                categoryId = categoryId,
            };

            return View(monthVM);
        }

        public ActionResult LineCategorySummary()
        {
            List<Expense> allExpenses = db.Expenses.ToList<Expense>();
            string[] categoryNames = { "Все категории" };

            IEnumerable<Month> months = from expense in allExpenses
                                        group expense by new { month = expense.date.Month } into m
                                        orderby m.Key.month
                                        select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            Month[] listMonths = new Month[months.Count()];

            for (int j = 0; j < months.Count(); j++)
            {
                Month month = months.ElementAt(j);
                decimal[] prices = new decimal[1];
                decimal price = allExpenses.Where(e => e.date.Month == month.month).Sum(e => e.price);
                prices[0] = price;

                month.prices = prices;
                listMonths[j] = month;
            }

            List<Category> categories = getCategories();

            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { name = "Суммарные траты", Id = -1 });
            categories.Insert(0, new Category { name = "Все категории", Id = 0 });

            MonthVM monthVM = new MonthVM()
            {
                months = listMonths,
                categories = categoryNames,
                Categories = new SelectList(categories, "Id", "name"),
                categoryId = -1,
            };

            return View(monthVM);
        }
    }
}