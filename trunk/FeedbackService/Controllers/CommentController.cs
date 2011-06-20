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
        // AJAX GET: /Feedback/Create

        //[AcceptAjaxAttribute]
        //public ActionResult AjaxCreate(/*Guid siteid*/)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Feedback feedback)
        //{
        //    return View(feedback);
        //}

        //
        // POST: /Feedback/Create

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.ClientIdRef = Helper.UserGuid();
                db.Feedbacks.Single(f => f.FeedbackId == comment.FeedbackId).Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Feedback", new { id = comment.FeedbackId });
            }

            return View(comment);
        }
    }
}
