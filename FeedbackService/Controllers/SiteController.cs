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
    public class SiteController : Controller
    {
        private FeedbackServiceContext db = new FeedbackServiceContext();

        public ActionResult All()
        {
            ViewBag.Title = "Все сайты";
            return View("Index", db.Sites.SetOwnerFlag());
        }
        
        [Authorize]
        public ViewResult Index()
        {
            ViewBag.Title = "Ваши сайты";
            return View(db.ClientForCurrentUser().Sites.SetOwnerFlag());
        }

        public ViewResult Details(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site.SetOwnerFlag());
        }

        [Authorize]
        public ViewResult Embed(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site);
        }

        [Authorize]
        public ActionResult Create()
        {
            Site site = new Site();
            site.SiteId = Guid.NewGuid();
            return View(site);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Site site)
        {
            if (ModelState.IsValid)
            {
                db.ClientForCurrentUser().Sites.Add(site);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(site);
        }

        [Authorize]
        public ActionResult Edit(Guid id)
        {
            Site site = db.Sites.Where(s => s.SiteId == id).First().SetOwnerFlag();
            if (!site.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            return View(site);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Site site)
        {
            site.ClientId = db.ClientForCurrentUser().ClientId;
            site.SetOwnerFlag();
            if (!site.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            if (ModelState.IsValid)
            {
                db.Entry(site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = site.SiteId });
            }
            return View(site);
        }

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            Site site = db.Sites.Where(s => s.SiteId == id).First().SetOwnerFlag();
            if (!site.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

            return View(site);
        }

        //
        // POST: /Site/Delete/5

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Site site = db.Sites.Where(s => s.SiteId == id).First().SetOwnerFlag();
            if (!site.isCurrentUserOwner)
            {
                throw new HttpException(403, Constants.Error403);
            }

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