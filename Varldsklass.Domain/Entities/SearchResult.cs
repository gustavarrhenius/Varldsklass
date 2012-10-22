using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Varldsklass.Domain.Entities
    {
    public class SearchResult
        {
        public string Title  { get; set; }
        public string Url { get; set; }
        public string Excerpt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        }
    }
