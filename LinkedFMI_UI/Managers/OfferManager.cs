using LinkedFMI_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkedFMI_UI.Enums;
using System.IO;

namespace LinkedFMI_UI.Managers
{
    public class OfferManager
    {
        private LinkedFMIDb db = new LinkedFMIDb();
        private UsersContext context = new UsersContext();

        public void AddOffer(string employerId)
        {
            Offer offer = new Offer();
            offer.EmployerId = employerId;
            offer.Title = "";
            offer.ReferenceNumber = "";
            offer.DailyWorkTime = new WorkTime();
            offer.Description = "";
            offer.OfferType = new LinkedFMI_UI.Enums.Type();
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
    }
}