using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Varldsklass.Domain.Entities.Abstract
{
    public interface IEntity
    {
        int ID { get; set; }
    }
}
