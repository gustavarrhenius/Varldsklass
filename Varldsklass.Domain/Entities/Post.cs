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
        public string Title { get; set; }
        public string Body { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }
        public Category Category  { get; set; }
    }
}