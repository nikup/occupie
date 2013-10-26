using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace LinkedFMI_UI.Helpers
{
    public static class Helpers
    {
        public static MvcHtmlString ActionImageLink(this AjaxHelper helper, string imageUrl, string imageClass, string imageId, string onclick, string actionName, string controller, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("class", imageClass);
            builder.MergeAttribute("id", imageId);
            builder.MergeAttribute("onclick", onclick);
            var link = helper.ActionLink("[replaceme]", actionName, controller, routeValues, ajaxOptions);
            return new MvcHtmlString(link.ToHtmlString().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }
    }
}