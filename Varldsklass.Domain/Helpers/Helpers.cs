using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Web.Mvc.Ajax;

namespace Varldsklass.Domain.Helpers
{
    public static class Helpers
    {
        public static string DateFormat(this HtmlHelper html, DateTime date)
        {
            string formatDate = date.ToString("dddd, dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("sv-SE"));

            return formatDate;
        }
    }
}