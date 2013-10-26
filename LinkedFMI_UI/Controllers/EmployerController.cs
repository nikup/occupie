using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkedFMI_UI.Models;
using LinkedFMI_UI.Managers;
using WebMatrix.WebData;
using System.IO;
using System.Web.Helpers;

namespace LinkedFMI_UI.Controllers
{
    public class EmployerController : Controller
    {
        private EmployerManager manager = new EmployerManager();
        private LinkedFMIDb db = new LinkedFMIDb();
        
        //
        // GET: /Employer/Profile

        public ActionResult Profile()
        {
            var employer = manager.GetEmployerByUserId(WebSecurity.CurrentUserId);
            return View(employer);
        }

        //
        // GET: /Employer/All

        public ActionResult All()
        {
            return View();
        }

        //
        // GET: /Employer/Edit?profileId=2

        public ActionResult Edit(int profileId)
        {
            // TO FIX: link contains id if student, find a better way of handling it
            int userId = WebSecurity.CurrentUserId;
            if (manager.GetProfileIdByUserId(userId) == profileId)
            {
                Employer employer = db.Employers.FirstOrDefault(p => p.EmployerProfileId == profileId);
                return View(employer);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Employer/Edit
        [HttpPost]
        public ActionResult Edit(Employer employer)
        {
            if (ModelState.IsValid)
            {
                manager.SaveEmployer(employer);
                return RedirectToAction("Profile");
            }
            else
            {
                return View(employer);
            }
        }
    }
}
