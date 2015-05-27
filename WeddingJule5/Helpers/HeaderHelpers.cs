using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WeddingJule.Helpers
{
    public enum HeaderVariant { ListExpense, ListCategory, ChartCategory, LineCategory }

    public static class HeaderHelpers
    {
        private static HeaderLinkInfo[] headerLinkInfos;
        private static UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

        static HeaderHelpers()
        {
            headerLinkInfos = new HeaderLinkInfo[4];
            headerLinkInfos[0] = new HeaderLinkInfo(HeaderVariant.ListExpense, "Expense", new { PageNumber = 1, category = 0 }, "Список затрат", urlHelper);
            headerLinkInfos[1] = new HeaderLinkInfo(HeaderVariant.ListCategory, "Category", null, "Список категорий", urlHelper);
            headerLinkInfos[2] = new HeaderLinkInfo(HeaderVariant.ChartCategory, "ChartCategory", null, "График распределения категорий", urlHelper);
            headerLinkInfos[3] = new HeaderLinkInfo(HeaderVariant.LineCategory, "LineCategory", null, "Затраты по месяцам", urlHelper);
        }

        public static MvcHtmlString HeaderLinks(this HtmlHelper html, HeaderVariant headerVariant)
        {
            StringBuilder result = new StringBuilder();

            foreach (HeaderLinkInfo headerLinkInfo in headerLinkInfos)
            {
                TagBuilder tag = new TagBuilder("a");

                tag.MergeAttribute("href", headerLinkInfo.urlString);
                tag.InnerHtml = headerLinkInfo.linkCaption;

                if (headerLinkInfo.headerVarinat == headerVariant)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }            

            return MvcHtmlString.Create(result.ToString());
        }
    }

    public class HeaderLinkInfo
    {
        public readonly HeaderVariant headerVarinat;
        public readonly string urlString;
        public readonly string Controller;
        public readonly string linkCaption;

        public HeaderLinkInfo(HeaderVariant headerVariant, string Controller, object routeValues, string linkCaption, UrlHelper urlHelper)
        {
            this.headerVarinat = headerVariant;
            this.Controller = Controller;
            this.urlString = urlHelper.Action(headerVariant.ToString(), Controller, routeValues);
            this.linkCaption = linkCaption;
        }
    }
}