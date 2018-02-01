using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class Helpers
    {
        public static IHtmlString IncludeCSS(this HtmlHelper html, string url)
        {
            string css = "<link rel=\"stylesheet\" href=\"{0}?{1}\" type=\"text/css\" />";
            return new HtmlString(string.Format(css, url, Intranet.Helpers.Sessions.ADUser == null ? DateTime.Now.ToString("ddMMyyyyHHmmss") : Intranet.Helpers.Sessions.ADUser.UniqueIdentifierLogin));
        }
        public static IHtmlString IncludeJS(this HtmlHelper html, string url)
        {
            string js = "<script type=\"text/javascript\" src=\"{0}?{1}\" ></script>";
            return new HtmlString(string.Format(js, url, Intranet.Helpers.Sessions.ADUser == null ? DateTime.Now.ToString("ddMMyyyyHHmmss") : Intranet.Helpers.Sessions.ADUser.UniqueIdentifierLogin));
        }
        public static IHtmlString IntranetTextBox(this HtmlHelper html, string id, string value, bool required)
        {
            return IntranetTextBox(html, id, value, required, string.Empty, false);
        }
        public static IHtmlString IntranetTextBox(this HtmlHelper html, string id, string value, bool required, string @class, bool disabled)
        {
            StringBuilder sbClass = new StringBuilder();
            string disabledTemp = string.Empty;

            sbClass.Append("txtInput");
            if (required)
            {
                sbClass.Append(" required");
            }
            if (@class.Length > 0)
            {
                sbClass.AppendFormat(" {0}", @class);
            }
            if (disabled)
            {
                sbClass.Append(" txtdisabled");
            }

            if (disabled)
            {
                disabledTemp = " readonly=\"readonly\"";
            }

            string input = string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\" class=\"{1}\" value=\"{2}\"{3} />",
                id,
                sbClass,
                value,
                disabledTemp);
            return new HtmlString(string.Format(input, id, value));
        }
    }
}