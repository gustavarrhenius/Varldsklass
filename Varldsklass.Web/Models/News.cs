using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Varldsklass.Web.Models
{
    public class News
    {
        [Required(ErrorMessage = "Fyll i rubrik")]
        public string Headline { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Fyll i ingress")]
        public string Ingress { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Fyll i text")]
        public string Text { get; set; }
    }
}