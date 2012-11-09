using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
{
    public class Account : IEntity
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Administrator { get; set; }

        public virtual ICollection<Attendant> Attendants { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
