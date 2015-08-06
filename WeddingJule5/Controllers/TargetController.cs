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

        public ActionResult save(Target[] targets)
        {
            foreach(Target target in targets)
            {
                if(target.id == null)
                {
                    db.Targets.Add(target);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(target).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return View("Target");
        }

        public JsonResult GetData()
        {
            return Json(db.Targets, JsonRequestBehavior.AllowGet);
        }
    }
}