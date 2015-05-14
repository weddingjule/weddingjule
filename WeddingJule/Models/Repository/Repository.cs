using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingJule.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Expense> Expenses
        {
            get { return context.expenses; }
        }
    }
}