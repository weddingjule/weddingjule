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
        [Display(Name = "Наименование траты")]
        public string name { get; set; }

        [Required(ErrorMessage = "Стоимость должна быть задана")]
        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Range(typeof(decimal), "0,01", "50000,00")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Дата должна быть задана")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата транзакции")]
        public DateTime date { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}   