using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Controllers
{
    public class DayActivityController : Controller
    {
        [HttpPost]
        public RedirectResult Index(JsonResult j)
        {
            return RedirectPermanent("/Angular/DayActivity.html");
        }
    }
}