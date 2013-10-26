using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkedFMI_UI.Models;
using LinkedFMI_UI.Enums;
using LinkedFMI_UI.ViewModels.OfferViewModels;

namespace LinkedFMI_UI.Controllers
{
    public class OfferController : Controller
    {
        private LinkedFMIDb db = new LinkedFMIDb();

        //
        // GET: /Offer/

        public ActionResult Profile()
        {
            return View(db.Offers.ToList());
        }

        // GET: /Offer/All
        public ActionResult All(OfferQueryViewModel query)
        {
            //var offers = db.Offers.Select(o => new OfferAllViewModel
            //{
            //    Id = o.OfferId,
            //    DailyWorkTime = o.DailyWorkTime,
            //    EmployerTitle = o.Employer.Name,
            //    OfferLevel = o.OfferLevel,
            //    OfferType = o.OfferType,
            //    //Technologies = o.MainTechnologies,
            //    Title = o.Title
            //});

            var allOffers = db.Offers.ToList();
            var offerViewModels = new List<OfferAllViewModel>();

            foreach (var offer in allOffers)
            {
                string offerTechnologies = string.Join(", ", offer.MainTechnologies.Take(5));
                string offerLevel = EnumTranslator.Levels[offer.OfferLevel];
                string offerType = EnumTranslator.Types[offer.OfferType];
                string offerWorkTime = EnumTranslator.WorkTimes[offer.DailyWorkTime];

                offerViewModels.Add(new OfferAllViewModel
                    {
                        Id = offer.OfferId,
                        DailyWorkTime = offerWorkTime,
                        EmployerTitle = offer.Title,
                        OfferLevel = offerLevel,
                        OfferType = offerType,
                        Title = offer.Title,
                        Technologies = offerTechnologies
                    });
            }

            return View(offerViewModels);

            //if (query == null)
            //{
            //    var offers = db.Offers.Select(o => new OfferAllViewModel
            //        {
            //            Id = o.OfferId,
            //            DailyWorkTime = o.DailyWorkTime,
            //            EmployerTitle = o.Employer.Name,
            //            OfferLevel = o.OfferLevel,
            //            OfferType = o.OfferType,
            //            Technologies = o.MainTechnologies,
            //            Title = o.Title
            //        });

            //    return View(offers);
            //}

            //throw new NotImplementedException();
        }

        //
        // GET: /Offer/Details/5

        public ActionResult Details(int id = 0)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        //
        // GET: /Offer/Create

        public ActionResult AddOffer()
        {
            return View();
        }

        //
        // POST: /Offer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOffer(Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("EmployerOffers", "Employer");
            }

            return View(offer);
        }

        //
        // GET: /Offer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        //
        // POST: /Offer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offer);
        }

        //
        // GET: /Offer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        //
        // POST: /Offer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public List<Offer> SearchOffers(List<string> technologies,
        //    List<WorkTime> workTimes,
        //    List<Level> levels,
        //    List<LinkedFMI_UI.Enums.Type> types)
        //{
        //    Func<Offer, bool> predicate = (offer) =>
        //        {
        //            bool hasTech = offer.MainTechnologies.Intersect(technologies).FirstOrDefault() != null;
        //            bool workHours = workTimes.Contains(offer.DailyWorkTime);
        //            bool level = levels.Contains(offer.OfferLevel);
        //            bool type = types.Contains(offer.OfferType);

        //            return hasTech && workHours && level && type;
        //        };

        //    List<Offer> result = db.Offers.Where(predicate).ToList();
        //    return result;
        //}
    }
}