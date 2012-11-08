using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;
using System.Web.Mvc;

namespace Varldsklass.Web.ViewModels
{
    public class PopularCoursesViewModel
    {
        public List<Post> Posts { get; set; }
        public int ID { get; set; }
        public int CourseOne { get; set; }
        public int CourseTwo { get; set; }
        public int CourseThree { get; set; }
        public int CourseFour { get; set; }
    }
}