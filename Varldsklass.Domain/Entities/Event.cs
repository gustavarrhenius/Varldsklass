using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Web.Mvc;
using System.ComponentModel;

namespace Varldsklass.Domain.Entities
    {
    public class Event : IEntity
        {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        public string Title { get; set; }
        [Required]
        public string Teatcher { get; set; }
        [Required]
        public string City { get; set; }

        [AllowHtml]
        public string Body { get; set; }
        public DateTime Created { get; set; }

        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Śtartdatum är obligatoriskt")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
        public DateTime EndDate { get; set; }

        public int PostID { get; set; }
        public virtual Post Post { get; set; }

        public virtual ICollection<Attendant> Attendants { get; set; }

        }
    }
