using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class SideBarViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
        public Post Post { get; set; }
    }
}