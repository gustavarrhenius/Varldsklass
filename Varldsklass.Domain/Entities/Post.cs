using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Web.Mvc;
namespace Varldsklass.Domain.Entities
{
    public class Post : IEntity
    {
        [HiddenInput(DisplayValue = false)]  
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        
        [AllowHtml]
        public string Body { get; set; }
        public DateTime Created;

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public int postType { get; set; }

        public enum PostType 
        {
            Course = 0,
            Page = 1
        }

        public Post()
        {
            Created = DateTime.Now;
        }           
    }
}