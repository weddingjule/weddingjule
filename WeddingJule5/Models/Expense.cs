using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }

        [Required(ErrorMessage = "Наименование траты не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Наименование")]
        public string name { get; set; }

        [Required(ErrorMessage = "Стоимость должна быть задана")]
        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость")]
        [Range(typeof(decimal), "0,01", "50000,00", ErrorMessage = "Некорректно ввели стоимость")]
        [RegularExpression(@"\d{1,6},\d{1,2}", ErrorMessage = "Формат $99999,99")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Дата должна быть задана")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата")]
        public DateTime date { get; set; }

        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}   