using LinkedFMI_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkedFMI_UI.Enums;
using System.IO;

namespace LinkedFMI_UI.Managers
{
    public class EmployerManager
    {
        private LinkedFMIDb db = new LinkedFMIDb();
        private UsersContext context = new UsersContext();  

        public void AddEmployer(int userId)
        {
            Employer employer = new Employer();
            employer.UserId = userId;

            string imageFile = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/default-avatar.jpg"));
            byte[] buffer = File.ReadAllBytes(imageFile);

            employer.Picture = buffer;

            //TODO fill info
            employer.Name = "Company";
            employer.Adress = "Kaspichan";
            employer.Website = "http://rakiya.com/";
            employer.YearFounded = 1993;
            employer.NumberOfEmployees = 15;
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
                dbEntry.Adress = employer.Adress;

                dbEntry.Website = employer.Website;
                dbEntry.LinkedIn = employer.LinkedIn;
                dbEntry.Email = employer.Email;
                dbEntry.Phone = employer.Phone;
                dbEntry.YearFounded = employer.YearFounded;
                dbEntry.Description = employer.Description;
                dbEntry.NumberOfEmployees = employer.NumberOfEmployees;

                foreach (string d in employer.Departments)
                {
                    dbEntry.Departments.Add(d);
                }

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

    }
}