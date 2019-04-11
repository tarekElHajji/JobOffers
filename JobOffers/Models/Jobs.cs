using identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobOffers.Models
{
    public class Jobs
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name ="Job Content")]
        public string JobContent { get; set; }

        [Required]
        [Display(Name ="Job Image")]
        public string JobImage { get; set; }

        [Required]
        [Display(Name = "Job Category")]
        public int CategoriesId { get; set; }

        public string UserId { get; set; }

        public virtual Categories Categorie { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}