using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class BookViewModel
    {
        public Event Event { get; set; }
        public List<Attendant> Attendants { get; set; }
    }
}