using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackService.Models;
using FeedbackService.Queries;
using System.Threading;
using Microsoft.Web.Mvc;

namespace FeedbackService.Controllers
{ 
    public class FeedbackController : Controller
    {
        private FeedbackServiceContext db = new FeedbackServiceContext();

        public ViewResult Details(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
                throw new HttpException(404, "Такого отзыва нет");

            return View(feedback.SetOwnerFlag());
        }

        public ActionResult Create(Guid id, string ReturnUrl)
        {
            Site site = db.Sites.Find(id);
            if (site == null)
                throw new HttpException(404, "Такого сайта нет");

            Feedback feedback = new Feedback();
            feedback.SiteId = id;
            feedback.SiteUrl = site.Url;
            //db.FeedbackTypes.Load();
            feedback.AllFeedbackTypes = db.FeedbackTypes;

            if (HttpContext.Request.IsAjaxRequest())
                return View("Create_Ajax", feedback);

            return View(feedback);
        }

        [HttpPost]
        public ActionResult Create(Feedback feedback, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                feedback.FeedbackId = Guid.NewGuid();
                feedback.Proposed = DateTime.Now;

                db.Sites.Single(s => s.SiteId == feedback.SiteId).Feedbacks.Add(feedback);
                db.SaveChanges();
                if (ReturnUrl != null)
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Details", "Site", new { id=feedback.SiteId });  
            }

            Site site = db.Sites.Find(feedback.SiteId);
            if (site == null)
                throw new HttpException(404, "Такого сайта нет");

            feedback.AllFeedbackTypes = db.FeedbackTypes;

            if (HttpContext.Request.IsAjaxRequest())
                return View("Create_Ajax", feedback);

            return View(feedback.SetOwnerFlag());
        }

        public ActionResult Vote(Guid id, bool isUp, string returnUrl)
        {
            if (db.VoteForFeedback(db.Feedbacks.Find(id), HttpContext.Request.Url.Host))
            {
                ViewBag.Message = "Спасибо за ваш голос";
            }
            else
            {
                ViewBag.Message = "Мы отозвали ваш голос";
            }
            db.SaveChanges();

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [Authorize]
        public ActionResult Edit(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id).SetOwnerFlag();
            if (!feedback.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            feedback.SetOwnerFlag();
            if (!feedback.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Feedback", new { id = feedback.FeedbackId });
            }
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id).SetOwnerFlag();
            if (!feedback.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            return View(feedback);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Feedback feedback = db.Feedbacks.Find(id).SetOwnerFlag();
            Guid retId = feedback.SiteId;
            if (!feedback.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            return RedirectToAction("Details", "Site", new { id = retId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public PartialViewResult TopFeedbacks()
        //{
        //    Thread.Sleep(2000);
        //    var feedbacks = db.Feedbacks.TopByRating(1).Single();
        //    return PartialView("_Feedback", feedbacks);
        //}

    //    public PartialViewResult Search(string q)
    //    {
    //        var feedbacks = db.Feedbacks
    //            .Where(r => r.Message.Contains(q) || String.IsNullOrEmpty(q))
    //            .Take(10);
    //        return PartialView("_FeedbackSearchResults", feedbacks);
    //    }

    //    public ActionResult QuickSearch(string term)
    //    {
    //        var feedbacks = db.Feedbacks
    //.Where(r => r.Message.Contains(term) || String.IsNullOrEmpty(term))
    //.Take(10)
    //.Select(r => new { label = r.Message });

    //        return Json(feedbacks, JsonRequestBehavior.AllowGet);
    //    }

    //    public ActionResult JsonSearch(string q)
    //    {
    //        var feedbacks = db.Feedbacks
    //.Where(r => r.Message.Contains(q) || String.IsNullOrEmpty(q))
    //.Take(10)
    //.Select(r => new { r.Message });

    //        return Json(feedbacks, JsonRequestBehavior.AllowGet);
    //    }


    }
}