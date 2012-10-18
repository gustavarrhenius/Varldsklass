using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class CalendarViewModel
    {
        public int ID { get; set; }

        public DateTime StartDate { get; set; }

        public string CourseTitle { get; set; }

        public string EventTitle { get; set; }

        public string City { get; set; }
    }
}