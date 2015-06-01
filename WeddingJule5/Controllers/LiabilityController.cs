using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using WeddingJule.Models.Liability;

namespace WeddingJule.Controllers
{
    public class LiabilityController : Controller
    {
        static ExpenseContext db = new ExpenseContext();

        // GET: Liability
        public ActionResult LiabilityIndex()
        {
            decimal totalPrice = db.Liabilities.Sum<Liability>(l=>l.price);
            LiabilityViewModel lvm = new LiabilityViewModel() { liabilities = db.Liabilities, totalPrice = totalPrice.ToString("C") };
            return View(lvm);
        }

        [HttpPost]
        public void Create(Liability liability)
        {
            if (ModelState.IsValid)
            {
                db.Liabilities.Add(liability);
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void Edit(Liability liability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liability).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        [HttpPost]
        public void Delete(Liability liability)
        {
            db.Liabilities.Attach(liability);
            db.Liabilities.Remove(liability);

            db.SaveChanges();
        }
    }

        public class Book
{
    public int Id { get;set;}
    public string Name { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Price { get; set; }
}
}