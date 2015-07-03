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
            CharData[] charDatas = null;
            if (month == Month.Proportion)
                charDatas = monthProportion();
            else
                charDatas = monthCategories(month);

            return View("ChartCategory", createChartCategory(month, charDatas));
        }

        public ChartCategory createChartCategory(int? month, CharData[] charDatas)
        {
            IEnumerable<Month> months = from expense in db.Expenses.ToList<Expense>()
                                        group expense by new { month = expense.date.Month } into m
                                        orderby m.Key.month
                                        select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            ChartCategory chartCategory = new ChartCategory() { charDatas = charDatas };
            chartCategory.month = month;
            chartCategory.months = monthSelectList(months);

            return chartCategory;
        }

        private SelectList monthSelectList(IEnumerable<Month> months)
        {
            List<Month> monthList = months.ToList<Month>();
            monthList.Insert(0, new Month() { month = null, monthName = "Все месяца" });
            monthList.Add(new Month() { month = Month.Proportion, monthName = "Пропорция" });
            return new SelectList(monthList, "month", "monthName");
        }

        public CharData[] monthProportion()
        {
            IEnumerable<Month> months = from expense in db.Expenses.ToList<Expense>()
                                        group expense by new { month = expense.date.Month } into m
                                        orderby m.Key.month
                                        select new Month() { month = m.Key.month, monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key.month) };

            int monthsCount = months.Count();
            CharData[] charDatas = new CharData[monthsCount];

            for (int j = 0; j < monthsCount; j++)
            {
                Month month = months.ElementAt(j);
                IEnumerable<Expense> monthExpenses = db.Expenses.Where(e => e.date.Month == month.month.Value).AsEnumerable();
                CharData charData = new CharData();
                charData.name = month.monthName;
                charData.price = monthExpenses.Sum(e => e.price);
                charDatas[j] = charData;
            }

            return charDatas;
        }

        public CharData[] monthCategories(int? month)
        {
            IEnumerable<Category> categories = db.Categories.AsEnumerable<Category>();
            CharData[] charDatas = new CharData[categories.Count()];

            for (int i = 0; i < charDatas.Length; i++)
            {
                Category category = categories.ElementAt<Category>(i);
                IEnumerable<Expense> expenses = null;
                CharData CharData = null;

                if (month == null)
                    expenses = db.Expenses.Where<Expense>(e => e.CategoryId == category.Id).AsEnumerable<Expense>();
                else
                    expenses = db.Expenses.Where<Expense>(e => e.CategoryId == category.Id && (month == e.date.Month)).AsEnumerable<Expense>();

                CharData = new CharData() { name = category.name, price = expenses.Sum(e => e.price) };
                charDatas[i] = CharData;
            }

            charDatas = charDatas.OrderByDescending(c => c.price).ToArray<CharData>();

            return charDatas;
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