using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class EditLocationsViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}