using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class StatisticsViewModel
    {
        public Event Event { get; set; }

        public double Teacher { get; set; }
        public double Location { get; set; }
        public double Food { get; set; }
        public double Overall { get; set; }

        public List<string> Opinions { get; set; }
    }
}