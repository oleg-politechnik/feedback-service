using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace FeedbackService.Models
{
    public class FeedbackType
    {
        public int FeedbackTypeId { get; set; }
        public Guid SiteId { get; set; }

        [Required]
        public string TypeName { get; set; }
    }
}
