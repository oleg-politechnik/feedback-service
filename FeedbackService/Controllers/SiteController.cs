using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackService.Models;
using FeedbackService.Queries;

namespace FeedbackService.Controllers
{
    [Authorize]
    public class SiteController : Controller
    {
        private FeedbackServiceContext db = new FeedbackServiceContext();

        //
        // GET: /Site/

        public ViewResult Index()
        {
            Site self = new Site();
            self.Url = "feedbackservice.rtf.ustu.ru";

            var g = new Guid(Constants.ThisSiteGuid);
            if (db.Sites.Where(s => s.SiteId == g).Count() == 0)
            {
                self.SiteId = new Guid(Constants.ThisSiteGuid);
                db.Clients.ClientForCurrentUser().Sites.Add(self);
                db.SaveChanges();
            }

            return View(db.Clients.ClientForCurrentUser().Sites);
        }

        public ViewResult All()
        {
            ViewBag.Message = "Все сайты";

            return View("Index", db.Sites);
        }

        //
        // GET: /Site/Details/5

        public ViewResult Details(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site);
        }

        //
        // GET: /Site/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Site/Create

        [HttpPost]
        public ActionResult Create(Site site)
        {
            site.SiteId = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                db.Clients.ClientForCurrentUser().Sites.Add(site);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(site);
        }
        
        //
        // GET: /Site/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site);
        }

        //
        // POST: /Site/Edit/5

        [HttpPost]
        public ActionResult Edit(Site site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        //
        // GET: /Site/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site);
        }

        //
        // POST: /Site/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Site site = db.Sites.Find(id);
            db.Sites.Remove(site);
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