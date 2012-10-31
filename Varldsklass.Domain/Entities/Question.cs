using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Varldsklass.Domain.Entities.Abstract;

namespace Varldsklass.Domain.Entities
    {
    public class Question : IEntity
        {
        public int EventID { get; set; }
        public virtual Event Event { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required]
        public int? Teacher { get; set; }

        [Required]
        public int? Location { get; set; }

        [Required]
        public int? Food { get; set; }

        [Required]
        public int? Overall { get; set; }

        [MaxLength(500, ErrorMessage = "Använd inte mer än 500 tecken")]
        public string Opinion { get; set; }

        }
    }
