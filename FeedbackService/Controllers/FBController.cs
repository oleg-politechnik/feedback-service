using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackService.Models;

namespace FeedbackService.Controllers
{ 
    public class FBController : Controller
    {
        private FeedbackServiceEntities db = new FeedbackServiceEntities();

        //
        // GET: /FB/

        public ViewResult Index()
        {
            var feedbacks = db.Feedbacks.Include("Estimation").Include("Site");
            return View(feedbacks.ToList());
        }

        //
        // GET: /FB/Details/5

        public ViewResult Details(Guid id)
        {
            Feedback feedback = db.Feedbacks.Single(f => f.FeedbackId == id);
            return View(feedback);
        }

        //
        // GET: /FB/Create

        public ActionResult Create()
        {
            ViewBag.EstimationLevel = new SelectList(db.Estimations, "EstimationLevel", "Title");
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url");
            return View();
        } 

        //
        // POST: /FB/Create

        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.FeedbackId = Guid.NewGuid();
                db.Feedbacks.AddObject(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.EstimationLevel = new SelectList(db.Estimations, "EstimationLevel", "Title", feedback.EstimationLevel);
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }
        
        //
        // GET: /FB/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Feedback feedback = db.Feedbacks.Single(f => f.FeedbackId == id);
            ViewBag.EstimationLevel = new SelectList(db.Estimations, "EstimationLevel", "Title", feedback.EstimationLevel);
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        //
        // POST: /FB/Edit/5

        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Attach(feedback);
                db.ObjectStateManager.ChangeObjectState(feedback, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstimationLevel = new SelectList(db.Estimations, "EstimationLevel", "Title", feedback.EstimationLevel);
            ViewBag.SiteId = new SelectList(db.Sites, "SiteId", "Url", feedback.SiteId);
            return View(feedback);
        }

        //
        // GET: /FB/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Feedback feedback = db.Feedbacks.Single(f => f.FeedbackId == id);
            return View(feedback);
        }

        //
        // POST: /FB/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Feedback feedback = db.Feedbacks.Single(f => f.FeedbackId == id);
            db.Feedbacks.DeleteObject(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}