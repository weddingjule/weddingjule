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
        [Display(Name = "Стоимость")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "Некорректно ввели стоимость")]
        [RegularExpression(@"\d{1,6}", ErrorMessage = "Введите от 1 до 5 цифр")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Дата должна быть задана")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата")]
        public DateTime date { get; set; }

        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}   