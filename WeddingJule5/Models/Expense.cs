﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        [Display(Name = "Наименование траты")]
        public string name { get; set; }
        [Display(Name = "Стоимость")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime date { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}   