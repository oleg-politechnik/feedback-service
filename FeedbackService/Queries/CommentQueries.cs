using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedbackService.Models;

namespace FeedbackService.Queries
{
    //public static class CommentQueries
    //{
    //    public static Comment SetOwnerFlag(this Comment comment)
    //    {
    //        if (comment.SiteId == null)
    //            return comment;

    //        FeedbackServiceContext db = new FeedbackServiceContext();

    //        comment.isCurrentUserOwner = ((db.Sites.Find(comment.SiteId).ClientId == Helper.UserGuid()) || Helper.IsRoot());
    //        return comment;
    //    }

    //    public static IQueryable<Comment> SetOwnerFlag(this IQueryable<Comment> comments)
    //    {
    //        foreach (Comment comment in comments)
    //        {
    //            comment.SetOwnerFlag();
    //        }
    //        return comments;
    //    }
    //}
}