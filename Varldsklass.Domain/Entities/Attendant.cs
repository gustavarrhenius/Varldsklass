using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Varldsklass.Domain.Entities
{
    public class Attendant : IEntity
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int EventID { get; set; }
        [Required]
        public int BookerID { get; set; }

        public virtual Event Event { get; set; }
        public virtual Account Account { get; set; }
    }
}
