﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;

namespace Varldsklass.Domain.Helpers
{
    public static class Helpers
    {
        public static string DateFormat(this HtmlHelper html, DateTime date)
        {
            string formatDate = date.ToString("dddd, dd MMMM, yyyy", CultureInfo.CreateSpecificCulture("sv-SE"));

            string capitalize = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formatDate);

            return capitalize;
        }
    }
}