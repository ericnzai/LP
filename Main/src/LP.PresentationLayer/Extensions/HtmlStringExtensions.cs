using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace LP.PresentationLayer.Extensions
{
    public static class HtmlStringExtensions
    {
        public static MvcHtmlString RenderTemplateInMvcHtmlString(this HtmlHelper helper, string template)
        {
            return MvcHtmlString.Create(template);
        }

        public static HtmlString RenderTemplateInHtmlString(this HtmlHelper helper, string template)
        {
            return  new HtmlString(template);
        }
    }
}