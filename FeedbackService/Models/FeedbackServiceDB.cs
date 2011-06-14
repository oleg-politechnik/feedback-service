using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FeedbackService.Models
{
    public class FeedbackServiceDB : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}