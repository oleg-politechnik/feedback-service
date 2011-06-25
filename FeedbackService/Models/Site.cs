using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FeedbackService.Models
{
    public class Site
    {
        [Required]
        [ScaffoldColumn(false)]
        public Guid SiteId { get; set; }

        [ScaffoldColumn(false)] 
        public Guid ClientId { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [RegularExpression(@"^([A-Za-z0-9-_]\.?)+\.[A-Za-z]{2,4}$", ErrorMessage = "Адрес задается в формате example.com")] 
        [Display(Name = "Веб-адрес")]
        public string Url { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [NotMapped]
        public bool isCurrentUserOwner { get; set; }
    }
}