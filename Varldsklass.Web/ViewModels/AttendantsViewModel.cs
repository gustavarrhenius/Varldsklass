using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class AttendantsViewModel
    {
        public virtual Event Event { get; set; }
        public virtual AttendingList AttendingList { get; set; }
    }
}