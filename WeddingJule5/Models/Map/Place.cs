using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.Map
{
    public class Place
    {
        public int Id { get; set; }
        [Display(Name="Наименование")]
        public string PlaceName { get; set; } // город
        public int Times { get; set; } // кол-во посещений
        [Display(Name = "Долгота")]
        public double GeoLong { get; set; } // долгота - для карт google
        [Display(Name = "Широта")]
        public double GeoLat { get; set; } // широта - для карт google
    }
}