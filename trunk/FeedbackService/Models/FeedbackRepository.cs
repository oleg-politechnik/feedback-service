using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Linq;
using FeedbackService.Models;

namespace FeedbackService.Models
{
    public class FeedbackRepository
    {
        Feedback fb = new Feedback();
        //fb.
        //private NerdDinnerEntities entities = new NerdDinnerEntities();
        ////
        //// Query Methods
        //public IQueryable<Dinner> FindAllDinners()
        //{
        //    return entities.Dinners;
        //}
        //public IQueryable<Dinner> FindUpcomingDinners()
        //{
        //    return from dinner in entities.Dinners
        //           where dinner.EventDate > DateTime.Now
        //           orderby dinner.EventDate
        //           select dinner;
        //}
        //public Dinner GetDinner(int id)
        //{
        //    return entities.Dinners.FirstOrDefault(d => d.DinnerID == id);
        //}
        ////
        //// Insert/Delete Methods
        //public void Add(Dinner dinner)
        //{
        //    entities.Dinners.AddObject(dinner);
        //}
        //public void Delete(Dinner dinner)
        //{
        //    foreach (var rsvp in dinner.RSVPs)
        //    {
        //        entities.RSVPs.DeleteObject(dinner.RSVPs);
        //    }
        //    entities.Dinners.DeleteObject(dinner);
        //}
        ////
        //// Persistence 
        //public void Save()
        //{
        //    entities.SaveChanges();
        //}
    }
}

