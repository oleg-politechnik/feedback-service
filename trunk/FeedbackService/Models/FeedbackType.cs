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
        public string TypeName { get; set; }
    }
}
