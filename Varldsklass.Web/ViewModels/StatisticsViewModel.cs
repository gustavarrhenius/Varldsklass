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

        public List<int> Teacher { get; set; }
        public List<int> Location { get; set; }
        public List<int> Food { get; set; }
        public List<int> Overall { get; set; }

        public List<string> Opinions { get; set; }

        public StatisticsViewModel()
        {
            Teacher = new List<int>();
            Location = new List<int>();
            Food = new List<int>();
            Overall = new List<int>();

            Opinions = new List<string>();
        }
    }
}