using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.IO;
using Occupie.ViewModels.OfferViewModels;

namespace Occupie.Managers
{
    public class OfferManager
    {
        private OccupieDb db = new OccupieDb();
        private UsersContext context = new UsersContext();

        public void AddOffer(string employerId)
        {
            Offer offer = new Offer();
            offer.EmployerId = employerId;
            offer.Title = "";
            offer.ReferenceNumber = "";
            offer.DailyWorkTime = new WorkTime();
            offer.Description = "";
            offer.OfferType = new Occupie.Enums.Type();
            offer.OfferLevel = new Level();
            offer.MainTechnologies = new List<string>();

            db.Offers.Add(offer);
            db.SaveChanges();
        }

        public void SaveOffer(Offer offer)
        {
            Offer dbEntry = db.Offers.Find(offer.OfferId);

            if (dbEntry != null)
            {
                dbEntry.Title = offer.Title;
                dbEntry.ReferenceNumber = offer.ReferenceNumber;
                dbEntry.DailyWorkTime = offer.DailyWorkTime;

                dbEntry.Description = offer.Description;
                dbEntry.OfferType = offer.OfferType;
                dbEntry.OfferLevel = offer.OfferLevel;
                dbEntry.Salary = offer.Salary;
                foreach (string t in offer.MainTechnologies)
                {
                    dbEntry.MainTechnologies.Add(t);
                }

                foreach (string t in offer.AdditionalTechnologies)
                {
                    dbEntry.AdditionalTechnologies.Add(t);
                }
            }

            db.SaveChanges();
        }

        public Offer GetOfferById(int id)
        {
            Offer offer = db.Offers.FirstOrDefault(x => x.OfferId == id);

            return offer;
        }

        public List<Offer> GetOfferByEmployerId(string id)
        {
            List<Offer> offers = db.Offers.Where(x => x.EmployerId == id).ToList();

            return offers;
        }

        public List<OfferAllViewModel> GetOffers()
        {
            var allOffers = db.Offers.ToList();
            var offerViewModels = new List<OfferAllViewModel>();

            foreach (var offer in allOffers)
            {
                if (offer.Employer == null)
                    continue;

                string offerTechnologies = string.Join(", ", offer.MainTechnologies.Take(5));
                string offerLevel = EnumTranslator.Levels[offer.OfferLevel];
                string offerType = EnumTranslator.Types[offer.OfferType];
                string offerWorkTime = EnumTranslator.WorkTimes[offer.DailyWorkTime];


                offerViewModels.Add(new OfferAllViewModel()
                {
                    Id = offer.OfferId,
                    DailyWorkTime = offerWorkTime,
                    EmployerTitle = offer.Employer.Name,
                    OfferLevel = offerLevel,
                    OfferType = offerType,
                    Title = offer.Title,
                    Technologies = offerTechnologies
                });
            }

            return offerViewModels;
        }
    }
}