using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
{
    public class Location : IEntity
    {
        public int ID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
    }
}