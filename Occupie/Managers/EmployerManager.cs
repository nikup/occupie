using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Occupie.Enums;
using System.IO;
using Occupie.ViewModels;

namespace Occupie.Managers
{
    public class EmployerManager
    {
        private OccupieDb db = new OccupieDb();
        private UsersContext context = new UsersContext();

        public void AddEmployer(int userId, RegisterModelEmployer model)
        {
            Employer employer = new Employer();
            employer.UserId = userId;

            string imageFile = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/company-avatar.png"));
            byte[] buffer = File.ReadAllBytes(imageFile);

            employer.Picture = buffer;

            employer.Name = model.Name;
            employer.AdminEmail = model.AdminEmail;
            employer.EIK = model.EIK;
            employer.Email = model.Email;
            employer.Website = model.Website;
            employer.Address = model.Address;

            db.Employers.Add(employer);

            db.SaveChanges();
        }

        public void SaveEmployer(Employer employer)
        {
            Employer dbEntry = db.Employers.Find(employer.EmployerProfileId);

            if (dbEntry != null)
            {
                if (employer.Picture != null)
                {
                    dbEntry.Picture = employer.Picture;
                }

                dbEntry.Name = employer.Name;
                dbEntry.Address = employer.Address;

                dbEntry.Website = employer.Website;
                dbEntry.LinkedIn = employer.LinkedIn;
                dbEntry.Email = employer.Email;
                dbEntry.Phone = employer.Phone;
                dbEntry.YearFounded = employer.YearFounded;
                dbEntry.Description = employer.Description;
                dbEntry.NumberOfEmployees = employer.NumberOfEmployees;

                dbEntry.DepartmentsString = employer.DepartmentsString;

                //TODO Add offers
            }

            db.SaveChanges();
        }

        public Employer GetEmployerByUserId(int id)
        {
            Employer employer = db.Employers.FirstOrDefault(x => x.UserId == id);

            return employer;
        }

        public int GetProfileIdByUserId(int id)
        {
            Employer employer = db.Employers.FirstOrDefault(x => x.UserId == id);

            return employer.EmployerProfileId;
        }

        public bool EmployerHasOffer(int offerId, int userId)
        {
            int profileId = GetProfileIdByUserId(userId);
            var employer = db.Employers.FirstOrDefault(x => x.EmployerProfileId == profileId);
            var offer = db.Offers.FirstOrDefault(x=>x.OfferId == offerId);

            if (employer.Offers.Contains(offer))
            {
                return true;
            }

            return false;
        }

        public List<EmployerAllViewModel> GetEmployers()
        {
            var employers = new List<EmployerAllViewModel>();

            foreach (var empl in db.Employers)
            {
                employers.Add(new EmployerAllViewModel
                {
                    Id = empl.UserId,
                    Name = empl.Name,
                    Picture = "data:image;base64," + System.Convert.ToBase64String(empl.Picture)
                });
            }

            return employers;
        }
    }
}