using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Varldsklass.Domain.Entities
{
    public class Attendant : IEntity
    {
        public int ID { get; set; }

        [DisplayName("Förnamn")]
        [Required(ErrorMessage = "Du måste ange förnamn")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn")]
        [Required(ErrorMessage = "Du måste ange efternamn")]
        public string LastName { get; set; }

        [DisplayName("E-post")]
        [Required(ErrorMessage = "Du måste ange en e-post-adress")]
        [MaxLength(254)]
        public string Email { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public int BookerID { get; set; } 

        public virtual Event Event { get; set; }
        public virtual Account Account { get; set; }

        
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
