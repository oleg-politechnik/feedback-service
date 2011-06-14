﻿using System;
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

        //
        // GET: /Feedback/

        //public ActionResult Index()
        //{
            //return RedirectToAction("Index
            //ViewBag.ip = HttpContext.Request.UserHostAddress;

            //var feedbacks = db.Feedbacks.Include(f => f.Site);
            //return View(feedbacks.ToList());
        //}

        //
        // GET: /Feedback/Details/5

        public ViewResult Details(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            return View(feedback);
        }

        //
        // GET: /Feedback/Create

        public ActionResult Create(Guid id)
        {


            Feedback feedback = new Feedback();
            feedback.SiteId = id;
            feedback.Types = db.FeedbackTypes;
            return View(feedback);
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
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.FeedbackId = Guid.NewGuid();
                feedback.Proposed = DateTime.Now;

                db.Sites.Single(s => s.SiteId == feedback.SiteId).Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Details", "Site", new { id=feedback.SiteId });  
            }

            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        [HttpPost]
        public ActionResult Vote(Guid id)
        {
            //HttpContext.Request.UserHostAddress
            return View();
        }
        
        //
        // GET: /Feedback/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        //
        // POST: /Feedback/Edit/5

        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        //
        // GET: /Feedback/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            return View(feedback);
        }

        //
        // POST: /Feedback/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult TopFeedbacks()
        {
            Thread.Sleep(2000);
            var feedbacks = db.Feedbacks.TopByRating(1).Single();
            return PartialView("_Feedback", feedbacks);
        }

        public PartialViewResult Search(string q)
        {
            var feedbacks = db.Feedbacks
                .Where(r => r.Message.Contains(q) || String.IsNullOrEmpty(q))
                .Take(10);
            return PartialView("_FeedbackSearchResults", feedbacks);
        }

        public ActionResult QuickSearch(string term)
        {
            var feedbacks = db.Feedbacks
    .Where(r => r.Message.Contains(term) || String.IsNullOrEmpty(term))
    .Take(10)
    .Select(r => new { label = r.Message });

            return Json(feedbacks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonSearch(string q)
        {
            var feedbacks = db.Feedbacks
    .Where(r => r.Message.Contains(q) || String.IsNullOrEmpty(q))
    .Take(10)
    .Select(r => new { r.Message });

            return Json(feedbacks, JsonRequestBehavior.AllowGet);
        }


    }
}