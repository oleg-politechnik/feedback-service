using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedbackService.Models;

namespace FeedbackService.Queries
{
    public static class ClientQueries
    {
        public static Client ClientForCurrentUser(this IQueryable<Client> clients)
        {
            Guid userGuid = Helper.UserGuid();
            return clients.Single(c => c.ClientId == userGuid);
        }
    }

    //public static class SiteQueries
    //{
    //    public static IEnumerable<FeedbackType> FeedbackTypes(this Site site)
    //    {
    //        return site 
    //    }
    //}

    public static class FeedbackQueries
    {
        public static IEnumerable<Feedback> TopByRating(this IQueryable<Feedback> feedbacks, int top)
        {
            return feedbacks.OrderByDescending(r => r.NumberRatesUp)
                .Take(top).ToList();
        }
    }   
}