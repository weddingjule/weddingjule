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
            
            foreach(Category category in categories)
            {
                IEnumerable<Expense> categoryExpenses = allExpenses.Where(p => p.CategoryId == category.Id);

                IEnumerable<CategoryWeekInfo> categoryWeekInfos = from p in categoryExpenses
                                                                  group p by new { month = p.date.Month } into m
                                                                  orderby m.Key.month
                                                                  select new CategoryWeekInfo(m.Key.month, m.Sum(c => c.price), category.name);

                CategoryWeekInfoViewModel cc = new CategoryWeekInfoViewModel(categoryWeekInfos);

                return View(cc);
            }



            return View();
        }
    }

    public class CategoryWeekInfoViewModel
    {
        public readonly IEnumerable<CategoryWeekInfo> categoryWeekInfos;
        public CategoryWeekInfoViewModel(IEnumerable<CategoryWeekInfo> categoryWeekInfos)
        {
            this.categoryWeekInfos = categoryWeekInfos;
        }
    }

    public class CategoryWeekInfo
    {
        public readonly int month;
        public readonly string monthName;
        public readonly decimal price;
        public readonly string categoryName;

        public CategoryWeekInfo(int month, decimal price, string categoryName)
        {
            this.month = month;
            this.monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            this.price = price;
            this.categoryName = categoryName;
        }
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