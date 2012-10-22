﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Varldsklass.Domain.Entities
{
    public class Category : IEntity
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett namn.")]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}