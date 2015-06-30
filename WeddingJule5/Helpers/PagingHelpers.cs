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
        private static string addPage(int i, int pageNumber, Func<int, string> pageUrl)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml = i.ToString();
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageNumber)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");

            return tag.ToString();
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            int pageMerge = 3;

            int minPage = Math.Max(pageInfo.PageNumber - pageMerge, 1);
            int maxPage = Math.Min((pageInfo.PageNumber + pageMerge), pageInfo.TotalPages);
            int difference = 6 - (maxPage - minPage);
            if(difference>0)
            {
                if (minPage == 1)
                    maxPage += Math.Min(difference, pageInfo.TotalPages - maxPage);
                else if (maxPage == pageInfo.TotalPages)
                    minPage -= Math.Max(difference, 0);
            }

            for (int i = minPage; i <= maxPage; i++)
                result.Append(addPage(i, pageInfo.PageNumber, pageUrl));

            return MvcHtmlString.Create(result.ToString());
        }
    }
}