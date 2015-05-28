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
    [Authorize]
    public class ChartCategoryController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: Chart
        public ActionResult ChartCategory(int? month)
        {
            IEnumerable<Category> categories = db.Categories.AsEnumerable<Category>();
            CharData[] charDatas = new CharData[categories.Count()];

            for (int i = 0; i < charDatas.Length; i++)
            {
                Category category = categories.ElementAt<Category>(i);
                IEnumerable<Expense> expenses = db.Expenses.Where<Expense>(e => e.CategoryId == category.Id && ((month.HasValue && month == e.date.Month) || !month.HasValue)).AsEnumerable<Expense>();
                CharData CharData = new CharData(){name = category.name, price = expenses.Sum(e=>e.price)};
                charDatas[i] = CharData;
            }

            List<Expense> allExpenses = db.Expenses.ToList<Expense>();
            IEnumerable<Month> months = from expense in allExpenses
                                        group expense by new { month = expense.date.Month } into m
                                        orderby m.Key.month
                                        select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            List<Month> monthList = months.ToList<Month>();
            monthList.Insert(0, new Month() { month = null, monthName = "Все месяца" });


            ChartCategory chartCategory = new ChartCategory() { charDatas = charDatas.OrderByDescending(c => c.price).AsEnumerable<CharData>() };
            chartCategory.months = new SelectList(monthList, "month", "monthName");
            chartCategory.month = month;

            return View("ChartCategory", chartCategory);
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