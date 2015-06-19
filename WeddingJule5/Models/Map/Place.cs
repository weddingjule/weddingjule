using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.Map
{
    public class Place
    {
        public int Id { get; set; }
        public string PlaceName { get; set; } // город
        public int Times { get; set; } // кол-во посещений
        public double GeoLong { get; set; } // долгота - для карт google
        public double GeoLat { get; set; } // широта - для карт google

    }
}