using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Varldsklass.Domain.Entities
{
    public class Location : IEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}