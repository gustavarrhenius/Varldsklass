using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
{
    public class Image : IEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Location> Locations { get; set; }

    }
}
