using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
    {
    public class EvaluationViewModel
        {
        public Event Event { get; set; }
        public Question Question { get; set; }
        }
    }