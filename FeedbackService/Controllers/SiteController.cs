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

        //
        // GET: /Site/
        
        [Authorize]
        public ViewResult Index()
        {
            var g = new Guid(Constants.ThisSiteGuid);
            if (db.Sites.Where(s => s.SiteId == g).Count() == 0)
            {
                Site site = new Site();
                site.Url = "feedbackservice.rtf.ustu.ru";
                site.Description = "Это сайт, на котором вы сейчас и находитесь.";
                site.SiteId = g;
                db.Clients.ClientForCurrentUser().Sites.Add(site);

                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Пожелание" });
                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Вопрос" });
                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Отчет об ошибке" });
                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Благодарность" });

                db.SaveChanges();
            }

            ViewBag.Title = "Ваши сайты";

            return View(db.Clients.ClientForCurrentUser().Sites.SetOwnerFlag());
        }

        //
        // GET: /Site/Details/5

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

        //
        // GET: /Site/Create

        [Authorize]
        public ActionResult Create()
        {
            Site site = new Site();
            site.SiteId = Guid.NewGuid();
            return View(site);
        }

        //
        // POST: /Site/Create

        [Authorize]
        [HttpPost]
        public ActionResult Create(Site site)
        {
            
            if (ModelState.IsValid)
            {
                db.Clients.ClientForCurrentUser().Sites.Add(site);

                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Пожелание" });
                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Вопрос" });
                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Отчет об ошибке" });
                db.FeedbackTypes.Add(new FeedbackType { SiteId = site.SiteId, TypeName = "Благодарность" });
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(site);
        }

        //
        // GET: /Site/Edit/5

        [Authorize]
        public ActionResult Edit(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site);
        }

        //
        // POST: /Site/Edit/5

        [Authorize]
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

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            Site site = db.Sites.Find(id);
            return View(site);
        }

        //
        // POST: /Site/Delete/5

        [Authorize]
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

        //===========================================//

        [ChildActionOnly]
        public ActionResult PartialSiteBox(Site site)
        {
            return View(site.SetOwnerFlag());
        }

        //[ChildActionOnly]
        //public ActionResult PartialSiteList(IEnumerable<Site> sites)
        //{
        //    return View(sites.SetOwnerFlag());
        //}
    }
}