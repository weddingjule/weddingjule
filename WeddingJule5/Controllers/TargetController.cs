using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers.Google_Chart
{
    public class TargetController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        // GET: Target
        public ActionResult Target()
        {
            return View();
        }

        public ActionResult add(Target target)
        {
            db.Targets.Add(target);
            db.SaveChanges();
            return View("Target");
        }

        public ActionResult update(Target target)
        {
            db.Entry(target).State = EntityState.Modified;
            db.SaveChanges();
            return View("Target");
        }

        public JsonResult GetData()
        {
            return Json(db.Targets, JsonRequestBehavior.AllowGet);
        }

        public ActionResult delete(Target target)
        {
            db.Targets.Attach(target);
            db.Targets.Remove(target);
            db.SaveChanges();

            return View("Target");
        }
    }
}