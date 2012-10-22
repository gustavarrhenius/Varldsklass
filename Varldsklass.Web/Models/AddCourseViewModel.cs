using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;
using System.Collections;

namespace Varldsklass.Web.Models
{
    public class AddCourseViewModel
    {  
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
        public List<Post> Posts { get; set; }

      
    }
}