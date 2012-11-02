using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Varldsklass.Web.ViewModels
{
    public class BookViewModel
    {
        public Event Event { get; set; }
        public Account Booker { get; set; }
        public List<Attendant> Attendants { get; set; }

        [Display(Name="Jag som bokar önskar också att deltaga")]
        public bool BookerAttends { get; set; }
    }
}