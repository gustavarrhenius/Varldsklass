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
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Role { get; set; }
    }
}
