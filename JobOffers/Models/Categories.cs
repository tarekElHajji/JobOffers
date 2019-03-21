using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class Categories
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name ="Category Description")]
        public string CategoryDescription { get; set; }

        public virtual IEnumerable<Jobs> Jobs { get; set; }
    }
}