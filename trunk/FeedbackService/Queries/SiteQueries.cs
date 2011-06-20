using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedbackService.Models;
using FeedbackService;

namespace FeedbackService.Queries
{
    public static class SiteQueries
    {
        public static Site SetOwnerFlag(this Site site)
        {
            site.isCurrentUserOwner = (site.ClientId == Helper.UserGuid());
            return site;
        }

        public static IQueryable<Site> SetOwnerFlag(this IQueryable<Site> sites)
        {
            foreach (Site site in sites)
            {
                site.SetOwnerFlag();
            }
            return sites;
        }

        public static ICollection<Site> SetOwnerFlag(this ICollection<Site> sites)
        {
            foreach (Site site in sites)
            {
                site.SetOwnerFlag();
            }
            return sites;
        }
    }
}