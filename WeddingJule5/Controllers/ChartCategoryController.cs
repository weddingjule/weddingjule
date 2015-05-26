using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using System.Data.Entity;
using System.Globalization;

namespace WeddingJule.Controllers
{
    public class ChartCategoryController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: Chart
        public ActionResult ChartCategory()
        {
            IEnumerable<Category> categories = db.Categories.AsEnumerable<Category>();
            CharData[] charDatas = new CharData[categories.Count()];

            for (int i = 0; i < charDatas.Length; i++)
            {
                Category category = categories.ElementAt<Category>(i);
                IEnumerable<Expense> expenses = db.Expenses.Where<Expense>(e => e.CategoryId == category.Id).AsEnumerable<Expense>();
                CharData CharData = new CharData(){name = category.name, price = expenses.Sum(e=>e.price)};
                charDatas[i] = CharData;
            }

            ChartCategory chartCategory = new ChartCategory(charDatas.OrderByDescending(c=>c.price).AsEnumerable<CharData>());

            return View("ChartCategory", chartCategory);
        }

        public ActionResult LineCategory()
        {
            List<Expense> allExpenses = db.Expenses.ToList<Expense>();
            List<Category> categories = db.Categories.ToList<Category>();
            string[] categoryNames = new string[categories.Count];

            IEnumerable<Month> months = from expense in allExpenses
                    group expense by new { month = expense.date.Month } into m
                    orderby m.Key.month
                    select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            Month[] listMonths = new Month[months.Count()];

            for (int j = 0; j < months.Count(); j++ )
            {
                Month month = months.ElementAt(j);
                decimal[] prices = new decimal[categories.Count];
                for (int i = 0; i < prices.Length; i++)
                {
                    Category category = categories.ElementAt(i);
                    categoryNames[i] = category.name;
                    decimal price = allExpenses.Where(e => e.CategoryId == category.Id && e.date.Month == month.month).Sum(e => e.price);
                    prices[i] = price;
                }

                IEnumerable<decimal> priceEnumerable = prices.AsEnumerable<decimal>();
                month.prices = priceEnumerable;
                listMonths[j] = month;
            }

            MonthVM monthVM = new MonthVM() { months = listMonths, categories = categoryNames };

            return View(monthVM);
        }
    }

    public class MonthVM
    {
        public IEnumerable<Month> months;
        public IEnumerable<string> categories;
    }

    public class Month
    {
        public int month;
        public string monthName;
        public IEnumerable<decimal> prices;
    }

    public static class JavaScriptConvert
    {
        public static IHtmlString SerializeObject(object value)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    // Let's use camelCasing as is common practice in JavaScript
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                // We don't want quotes around object names
                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }
    }
}