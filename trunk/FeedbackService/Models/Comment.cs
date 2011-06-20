using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FeedbackService.Models
{
    public class Comment
    {
        public Comment()
        {
            Timestamp = DateTime.Now;
        }

        [ScaffoldColumn(false)]
        public int CommentId { get; set; }

        [ScaffoldColumn(false)]
        public int ReplyToId { get; set; }

        [ScaffoldColumn(false)]
        public Guid FeedbackId { get; set; }

        [ScaffoldColumn(false)]
        public Guid ClientIdRef { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Timestamp { get; set; }
    }
}