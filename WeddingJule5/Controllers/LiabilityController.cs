using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    public class LiabilityController : Controller
    {
        static ExpenseContext db = new ExpenseContext();

        // GET: Liability
        public ActionResult LiabilityIndex()
        {
            decimal totalPrice = db.Liabilities.Sum<Liability>(l => l.price);
            LiabilityViewModel lvm = new LiabilityViewModel() { liabilities = db.Liabilities, totalPrice = totalPrice.ToString("C") };
            return View(lvm);
        }

        [HttpGet]
        public ActionResult CreateLiability(int? id)
        {
            return View("CreateLiability");
        }

        [HttpPost]
        public ActionResult CreateLiability(Liability liability)
        {
            if (ModelState.IsValid)
            {
                db.Liabilities.Add(liability);
                db.SaveChanges();
                return PartialView("Success");
            }
            else
            {
                int? id = null;
                return CreateLiability(id);
            }
        }

        [HttpGet]
        public ActionResult EditLiability(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Liability liability = db.Liabilities.Find(id);
            if (liability != null)
            {
                return PartialView(liability);
            }

            return RedirectToAction("LiabilityIndex");
        }

        [HttpPost]
        public ActionResult EditLiability(Liability liability)
        {
            if (ModelState.IsValid)
            {
                Liability dbLiability = db.Liabilities.Find(liability.LiabilityID);
                dbLiability.name = liability.name;
                dbLiability.price = liability.price;
                db.Entry(dbLiability).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("Success");
            }

            return PartialView(liability);
        }

        [HttpGet]
        public ActionResult DeleteLiability(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Liability liability = db.Liabilities.Find(id);

            if (liability != null)
                return PartialView(liability);

            return RedirectToAction("LiabilityIndex");
        }

        [HttpPost]
        public ActionResult DeleteLiability(Liability liability)
        {
            Liability dbLiability = db.Liabilities.Find(liability.LiabilityID);
            db.Liabilities.Remove(dbLiability);
            db.SaveChanges();
            return RedirectToAction("LiabilityIndex");
        }

        [HttpGet]
        public ActionResult TransferLiability(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Liability liability = db.Liabilities.Find(id);


            if (liability != null)
            {
                TransferLiabilityViewModel tlvm = new TransferLiabilityViewModel();

                Expense expense = new Expense();
                expense.name = liability.name;
                expense.price = liability.price;
                expense.Category = db.Categories.First<Category>();

                tlvm.expense = expense;
                tlvm.Categories = new SelectList(db.Categories, "Id", "name");
                tlvm.LiabilityID = liability.LiabilityID;
                tlvm.expense.date = DateTime.Today;

                return PartialView(tlvm);
            }

            return RedirectToAction("LiabilityIndex");
        }

        [HttpPost]
        public ActionResult TransferLiability(TransferLiabilityViewModel tlvm)
        {
            if (ModelState.IsValid)
            {
                Liability dbLiability = db.Liabilities.Find(tlvm.LiabilityID);
                db.Liabilities.Remove(dbLiability);
                db.Expenses.Add(tlvm.expense);
                db.SaveChanges();
                return PartialView("Success");
            }

            return PartialView(tlvm);
        }

    }
}