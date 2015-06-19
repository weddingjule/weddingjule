using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Controllers
{
    public class MapController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        public ActionResult MapIndex()
        {
            return View();
        }

        public JsonResult GetData()
        {
            return Json(db.Places, JsonRequestBehavior.AllowGet);
        }
    }
}