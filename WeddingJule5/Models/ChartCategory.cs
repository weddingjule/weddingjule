using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingJule.Models
{
    public class ChartCategory
    {
        public IEnumerable<CharData> charDatas { get; private set; }

        public ChartCategory(IEnumerable<CharData> charDatas)
        {
            this.charDatas = charDatas;
        }
    }

    public class CharData
    {
        public string name { get; set; }
        public decimal price { get; set; }
    }
}