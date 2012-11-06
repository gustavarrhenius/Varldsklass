using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.ViewModels
{
    public class FileUploadViewModel
    {
        public Post post { get; set; }
        public Category Category { get; set; }
        public Event Event { get; set; }
        public List<UploadedFile> UploadedFiles { get; set; }
    }
}