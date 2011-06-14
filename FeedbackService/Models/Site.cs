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

        [Required]
        [ScaffoldColumn(false)] 
        public Guid ClientId { get; set; }

        [Required]
        [DataType(DataType.Url, ErrorMessage = "Какой-то у вас адрес неправильный")]
        [Display(Name = "Веб-адрес")]
        public string Url { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }

    //public class SiteFeedbackViewModel
    //{
    //    public Site Site { get; set; }
    //    public int MyProperty { get; set; }
    //}
}