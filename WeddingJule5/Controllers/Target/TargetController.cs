using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using WeddingJule.Models.TargetFolder;

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

        public JsonResult GetData(bool? showNotDoneTarget)
        {
            if (showNotDoneTarget == null || showNotDoneTarget.Value)
            {
                IEnumerable<Target> targets = db.Targets.Where<Target>(t => t.done == false).AsEnumerable<Target>();
                return Json(targets, JsonRequestBehavior.AllowGet);
            }
            else if (!showNotDoneTarget.Value)
                return Json(db.Targets, JsonRequestBehavior.AllowGet);
            else
                return null;
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