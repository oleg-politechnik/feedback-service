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

        [DataType(DataType.MultilineText)]
        [DisplayName("Текст сообщения")]
        [StringLength(200, ErrorMessage = "Попробуйте написать покороче, можете пояснить потом в комментариях")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Предложено")]
        public DateTime Proposed { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Голосов")]
        public uint NumberRatesUp { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual Site Site { get; set; }
        public virtual IEnumerable<FeedbackVote> Votes { get; set; }
        public virtual IEnumerable<FeedbackType> Types { get; set; }
        
        //public IEnumerable<ValidationResult>
        //    Validate(ValidationContext validationContext)
        //{
        //    var field = new[] { "voteip" };
        //    //if voteIP
        //    yield return new ValidationResult("Achtung", field);
        //}

        //+moderation, e-mail validation
    }

    public class FeedbackType
    {
        public int FeedbackTypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class FeedbackVote
    {
        public Guid FeedbackId { get; set; }
        public int FeedbackVoteId { get; set; }
        public string IpAddress { get; set; }
    }
}