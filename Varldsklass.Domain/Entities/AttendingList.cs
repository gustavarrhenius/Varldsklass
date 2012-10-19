using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
{
    public class AttendingList : IEntity
    {
        public int ID { get; set; }

        public Account Booker { get; set; }
        public bool BookerIsAttending { get; set; }

        public List<Attendant> Attendants { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
