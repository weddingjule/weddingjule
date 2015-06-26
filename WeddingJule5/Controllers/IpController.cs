using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ubs.GeoBazaAPI;

namespace WeddingJule.Controllers
{
    public class IpController : Controller
    {
        public ActionResult Location()
        {
            // получаем ip клиента (если не локальный хост)
            string IP = HttpContext.Request.UserHostAddress;
            string coordinates = "";
            // получаем географию
            ViewBag.Location = DefineLocation(IP, ref coordinates);
            ViewBag.Coords = coordinates;
            ViewBag.Ip = IP;
            return View();
        }

        // определяем местоположение, обращаясь к API и базе данных
        protected string DefineLocation(string IP, ref string coordinates)
        {
            GeoBazaAPI geo = new GeoBazaAPI(Server.MapPath("~/BD/geobaza.dat"));
            string result = "Не определено";
            // получаем географию по ip
            List<IPLocation> locList = geo.GetLocationByIP(IP);
            if (locList != null && locList.Count != 0 && locList[0].ID != -1)
            {
                IPLocation country = GetCountry(locList);

                if (country != null)
                    result = country.ISOID + ", " + country.NameRU + ", " + locList[0].NameRU + ", долгота: " + locList[0].Longitude + ", долгота: " + locList[0].Latitude;
                else
                    result = locList[0].NameRU + ", долгота: " + locList[0].Longitude + ", долгота: " + locList[0].Latitude;
                coordinates = locList[0].Latitude + ", " + locList[0].Longitude;
            }

            return result;
        }
        // определяем страну
        private IPLocation GetCountry(List<IPLocation> locList)
        {
            for (int i = 0; i < locList.Count; i++)
            {
                if (locList[i].Type == LocationType.Country)
                    return locList[i];
            }
            return null;
        }
    }
}