using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Web.Mvc.Ajax;
using System.Text.RegularExpressions;

namespace Varldsklass.Domain.Helpers
{
    public static class Helpers
    {
        public static string DateFormat(this HtmlHelper html, DateTime date)
        {
            string formatDate = date.ToString("dddd, dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("sv-SE"));

            return formatDate;
        }

        private static Regex regExHttpLinks = new Regex(@"(?<=\()\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|](?=\))|(?<=(?<wrap>[=~|_#]))\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|](?=\k<wrap>)|\b(https?://|www\.)[-A-Za-z0-9+&@#/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#/%=~_()|]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static string FormatHtml(this HtmlHelper htmlHelper, string html)
            {
            if (string.IsNullOrEmpty(html))
                {
                return html;
                }

            html = htmlHelper.Encode(html);
            html = html.Replace(Environment.NewLine, "<br />");

            // replace periods on numeric values that appear to be valid domain names
            var periodReplacement = "[[[replace:period]]]";
            html = Regex.Replace(html, @"(?<=\d)\.(?=\d)", periodReplacement);

            // create links for matches
            var linkMatches = regExHttpLinks.Matches(html);
            for (int i = 0; i < linkMatches.Count; i++)
                {
                var temp = linkMatches[i].ToString();

                if (!temp.Contains("://"))
                    {
                    temp = "http://" + temp;
                    }

                html = html.Replace(linkMatches[i].ToString(), String.Format("<a href=\"{0}\" title=\"{0}\">{1}</a>", temp.Replace(".", periodReplacement).ToLower(), linkMatches[i].ToString().Replace(".", periodReplacement)));
                }

            // Clear out period replacement
            html = html.Replace(periodReplacement, ".");

            return html;
            }
    }
}
