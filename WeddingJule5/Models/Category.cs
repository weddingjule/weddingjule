using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Категория траты")]
        public string name { get; set; }
        public IEnumerable<Expense> expenses { get; set; }
        //проверяем наличие чекина
    }
}  