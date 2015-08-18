using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using WeddingJule.Models.TargetFolder;

namespace WeddingJule.Controllers.TargetFolder
{
    public class PurchaseController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: Purchase
        public ActionResult PurchaseList()
        {
            return View();
        }

        public ActionResult add(Purchase purchase)
        {
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return View("Target");
        }

        public JsonResult GetData()
        {
            return Json(db.Purchases, JsonRequestBehavior.AllowGet);
        }

        public ActionResult delete(Purchase purchase)
        {
            db.Purchases.Attach(purchase);
            db.Purchases.Remove(purchase);
            db.SaveChanges();

            return View("Target");
        }
    }
}