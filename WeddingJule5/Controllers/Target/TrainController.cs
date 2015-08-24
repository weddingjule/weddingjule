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
    public class TrainController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: Train
        public ActionResult Train()
        {
            return View();
        }

        public ActionResult add(Train train)
        {
            db.Trains.Add(train);
            db.SaveChanges();
            return View("Train");
        }

        public ActionResult update(Train train)
        {
            db.Entry(train).State = EntityState.Modified;
            db.SaveChanges();
            return View("Train");
        }

        public JsonResult GetData()
        {
            return Json(db.Trains, JsonRequestBehavior.AllowGet);
        }

        public ActionResult delete(Train train)
        {
            db.Trains.Attach(train);
            db.Trains.Remove(train);
            db.SaveChanges();

            return View("Train");
        }
    }
}