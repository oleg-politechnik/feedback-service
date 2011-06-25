using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackService.Models;

namespace FeedbackService.Controllers
{
    public class FeedController : Controller
    {
        FeedbackServiceContext db = new FeedbackServiceContext();

        public ActionResult Feedback(Guid id)
        {
            return View(db.Feedbacks.Find(id));
        }
    }
}
