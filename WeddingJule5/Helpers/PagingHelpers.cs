using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeddingJule.Models;

namespace WeddingJule.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            if (pageInfo.TotalPages > 1)
            {
                for (int i = 1; i <= pageInfo.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    tag.AddCssClass("paging");
                    // если текущая страница, то выделяем ее,
                    // например, добавляя класс
                    if (i == pageInfo.PageNumber)
                        tag.MergeAttribute("style", "color:red");
                    result.Append(tag.ToString());
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}