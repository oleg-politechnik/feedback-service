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
        [RegularExpression(@"^([A-Za-z0-9]\.?)+[A-Za-z]{2,4}$", ErrorMessage = "Адрес задается в формате example.com")] 
        [Display(Name = "Веб-адрес")]
        public string Url { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public int MyProperty { get; set; }
        public int MyProperty22 { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        //public virtual ICollection<FeedbackType> FeedbackTypes { get; set; }

        [NotMapped]
        public bool isCurrentUserOwner { get; set; }

        //[NotMapped]
        //public int FeedbacksCount { get; set; }
    }

    //public class SiteFeedbackViewModel
    //{
    //    public Site Site { get; set; }
    //    public int MyProperty { get; set; }
    //}
}