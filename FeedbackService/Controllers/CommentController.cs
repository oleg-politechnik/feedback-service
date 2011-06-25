using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackService.Models;

namespace FeedbackService.Controllers
{
    public class CommentController : Controller
    {
        private FeedbackServiceContext db = new FeedbackServiceContext();

        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(Guid id)
        {
            Comment comment = new Comment();
            comment.FeedbackId = id;
            return View(comment);
        }

        //
        // POST: /Feedback/Create

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentId = Guid.NewGuid();
                comment.ClientIdRef = Helper.UserGuid();
                db.Feedbacks.Single(f => f.FeedbackId == comment.FeedbackId).Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Feedback", new { id = comment.FeedbackId });
            }

            return View(comment);
        }


        [Authorize]
        public ActionResult Delete(Guid id)
        {
            Comment comment = db.Comments.Find(id);
            if (!(comment.Feedback.Site.Client.ClientId == Helper.UserGuid()))
            {
                throw new HttpException(403, Constants.Error403);
            }

            return View(comment);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Comment comment = db.Comments.Find(id);
            Guid retId = comment.FeedbackId;

            if (!(comment.Feedback.Site.Client.ClientId == Helper.UserGuid()))
            {
                throw new HttpException(403, Constants.Error403);
            }

            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Feedback", new { id = retId });
        }
    }
}
