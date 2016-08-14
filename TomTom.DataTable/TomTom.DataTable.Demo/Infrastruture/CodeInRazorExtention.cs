using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;


namespace System.Web.Mvc
{
    public static class CodeInRazorExtention
    {
        public static IHtmlString Code(this HtmlHelper html, string path)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return html.Raw(File.ReadAllText(HostingEnvironment.MapPath(path)));
        }
    }
}