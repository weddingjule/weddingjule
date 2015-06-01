using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.Liability
{
    public class Liability
    {
        public int LiabilityID { get; set; }

        [Required(ErrorMessage = "Наименование обязательства не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Наименование")]
        public string name { get; set; }

        [Required(ErrorMessage = "Стоимость должна быть задана")]
        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Range(typeof(decimal), "0,01", "50000,00")]
        public decimal price { get; set; }
    }
}