using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace FeedbackService.Models
{
    public class Feedback /*: IValidatableObject*/
    {
        public Feedback()
        {
            Proposed = DateTime.Now;
        }

        [ScaffoldColumn(false)]
        public Guid FeedbackId { get; set; } //

        [ScaffoldColumn(false)]
        public Guid SiteId { get; set; } //

        [Required]
        [DisplayName("Тип отзыва")]
        public int FeedbackTypeId { get; set; }

        [Required]
        [DisplayName("Заголовок")]
        [StringLength(50, ErrorMessage = "Напишите подробнее в тексте сообщения")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Текст сообщения")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "Попробуйте написать покороче, можете пояснить потом в комментариях")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Proposed { get; set; }

        [DisplayName("Рейтинг")]
        [ScaffoldColumn(false)]
        public int Rating { get; set; }

        [ScaffoldColumn(false)]
        public bool Accepted { get; set; }

        public virtual Site Site { get; set; }
        public virtual FeedbackType FeedbackType { get; set; }
        public virtual ICollection<FeedbackVote> Votes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public IEnumerable<FeedbackType> AllFeedbackTypes { get; set; }

        [NotMapped]
        public string ReturnUrl { get; set; }

        [NotMapped]
        public string SiteUrl { get; set; }

        [NotMapped]
        public bool isCurrentUserOwner { get; set; }

        //public IEnumerable<ValidationResult>
        //    Validate(ValidationContext validationContext)
        //{
        //    var field = new[] { "voteip" };
        //    //if voteIP
        //    yield return new ValidationResult("Achtung", field);
        //}

        //+moderation, e-mail validation
    }

    public class FeedbackStatus
    {
        public int FeedbackStatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class FeedbackVote
    {
        public FeedbackVote() { IsUp = false; }

        public int FeedbackVoteId { get; set; }
        public Guid FeedbackId { get; set; }
        public string IpAddress { get; set; }
        public bool IsUp { get; set; }
    }
}