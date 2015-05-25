using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

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