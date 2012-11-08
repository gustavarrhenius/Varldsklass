using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
{
    public class PopularCourse : IEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required]
        public int CourseOne { get; set; }

        [Required]
        public int CourseTwo { get; set; }

        [Required]
        public int CourseThree { get; set; }

        [Required]
        public int CourseFour { get; set; }
    }
}
