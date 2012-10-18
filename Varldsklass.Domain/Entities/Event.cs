﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Web.Mvc;

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
        public DateTime Created;
        [Required]
        public DateTime StartDate;
        [Required]
        public DateTime EndDate;

        public int PostID { get; set; }
        public virtual Post Post { get; set; }

        public Event()
            {
            Created = DateTime.Now;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            }
        }
    }
