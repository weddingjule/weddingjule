using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.TargetFolder
{
    public class Train
    {
        public int? id { get; set; }
        [Required]
        public string name { get; set; }
        public bool done { get; set; }
    }
}