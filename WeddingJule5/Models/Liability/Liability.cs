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
        [Range(typeof(decimal), "0,01", "50000,00", ErrorMessage = "Некорректно ввели стоимость")]
        [RegularExpression(@"\d{1,6}(,\d{1,2})?", ErrorMessage = "Формат $99999,99")]
        public decimal price { get; set; }
    }
}