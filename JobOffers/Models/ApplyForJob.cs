﻿using identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobsId { get; set; }
        public string UserId { get; set; }

        public virtual Jobs Job { get; set; }
        public virtual  ApplicationUser User { get; set; }
    }
}