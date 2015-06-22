using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;
using WeddingJule.Models.PictureFolder;

namespace WeddingJule.Controllers
{
    public class PictureController : Controller
    {
        ExpenseContext db = new ExpenseContext();

        public ActionResult Gallery()
        {
            return View(db.Pictures);
        } 
    }
}