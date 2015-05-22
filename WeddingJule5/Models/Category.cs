using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Remote("CheckCategoryName", "Category", ErrorMessage = "Такая категория уже существует!")]
        [Required(ErrorMessage = "Наименование категории не может быть пустым")]
        [StringLength(30, MinimumLength=3, ErrorMessage="Наименование категории должно быть от 3 до 30 символов")]
        [Display(Name = "Категория траты")]
        public string name { get; set; }

        public IEnumerable<Expense> expenses { get; set; }
        //проверяем наличие чекина
    }
}  