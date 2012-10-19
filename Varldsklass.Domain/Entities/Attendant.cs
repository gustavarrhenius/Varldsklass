using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
{
    public class Attendant : IEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public int EventID { get; set; }
        public int BookerID { get; set; }
    }
}
