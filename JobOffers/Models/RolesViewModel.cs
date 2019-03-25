using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class RolesViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}