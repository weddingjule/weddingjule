using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.TargetFolder
{
    public class Purchase
    {
        public int purchaseId { get; set; }

        [Required(ErrorMessage = "Наименование траты не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Наименование")]
        public string name { get; set; }
    }
}