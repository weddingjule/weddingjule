using DollarApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingJule.Models;

namespace DollarApi.Controllers
{
    public class ValuesController : ApiController
    {
        static List<Dollar> dollars = new List<Dollar>();

        public IEnumerable<Dollar> GetBooks()
        {
            return dollars;
        }

        public Dollar GetBook(int id)
        {
            Dollar dollar = dollars.Where(d => d.id == id).Single();
            return dollar;
        }

        [HttpPost]
        public void CreateBook([FromBody]Dollar dollar)
        {
            dollar.id = dollars.Count;
            dollars.Add(dollar);
        }

        [HttpPut]
        public void EditBook(int id, [FromBody]Dollar dollar)
        {
            if (id == dollar.id)
            {
                Dollar dd = dollars.Where(d => d.id == id).Single();
                dd.name = dollar.name;
                dd.price = dollar.price;
            }
        }

        public void DeleteBook(int id)
        {
            Dollar dollar = dollars.Where(d => d.id == id).Single();
            if (dollar != null)
                dollars.Remove(dollar);
        }
    }
}
