using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedbackService.Models;

namespace FeedbackService.Queries
{
    public static class FeedbackQueries
    {
        //public static IEnumerable<Feedback> TopByRating(this IQueryable<Feedback> feedbacks, int top)
        //{
        //    return feedbacks.OrderByDescending(r => r.Rating)
        //        .Take(top).ToList();
        //}

        

        public static Feedback SetOwnerFlag(this Feedback feedback)
        {
            if (feedback.SiteId == null)
                return feedback;

            FeedbackServiceContext db = new FeedbackServiceContext();

            feedback.isCurrentUserOwner = (db.Sites.Find(feedback.SiteId).ClientId == Helper.UserGuid());
            return feedback;
        }

        public static IQueryable<Feedback> SetOwnerFlag(this IQueryable<Feedback> feedbacks)
        {
            foreach (Feedback feedback in feedbacks)
            {
                feedback.SetOwnerFlag();
            }
            return feedbacks;
        }
    }   
}