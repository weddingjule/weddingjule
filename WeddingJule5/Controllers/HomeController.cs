﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingJule5.Models;
using System.Data.Entity;

namespace WeddingJule5.Controllers
{
    public class HomeController : Controller
    {
        ExpenseContext db = new ExpenseContext();
         
        // Выводим всех футболистов
        public ActionResult Index()
        {
            var expenses = db.Expenses.Include(p => p.Category);
            //var expenses = db.Expenses;
            return View(expenses.ToList());
        }

        public ActionResult ListCategories(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            category.expenses = db.Expenses.Where(m => m.CategoryId == category.Id);
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "name");
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            db.Expenses.Add(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Expense expense = db.Expenses.Find(id);
            if (expense != null)
            {
                SelectList categories = new SelectList(db.Categories, "Id", "name");
                ViewBag.Categories = categories;
                return View(expense);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            db.Entry(expense).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Expense expense = db.Expenses.Find(id);
            Category category = db.Categories.Find(expense.CategoryId);
            expense.Category = category;

            if (expense != null)
                return View(expense);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Expense expense)
        {
            db.Expenses.Attach(expense);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}