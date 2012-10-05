using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Varldsklass.Domain.Entities
{
    class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int PostID { get; set; }
        public virtual Post Post { get; set; }
    }
}
