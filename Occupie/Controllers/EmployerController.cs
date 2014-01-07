using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Occupie.Models;
using Occupie.Managers;
using WebMatrix.WebData;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Occupie.ViewModels;
using System.Web.Helpers;

namespace Occupie.Controllers
{
    public class EmployerController : BaseController
    {
        //
        // GET: /Employer/Profile

        public ActionResult Profile(int id = 0)
        {
            Employer employer;
            if (id == 0)
            {
                employer = employerManager.GetEmployerByUserId(WebSecurity.CurrentUserId);
            }
            else
            {
                employer = employerManager.GetEmployerByUserId(id);
            }

            return View(employer);
        }


        //
        // GET: /Employer/Edit

        public ActionResult Edit()
        {
            Employer employer = employerManager.GetEmployerByUserId(WebSecurity.CurrentUserId);
            return View(employer);            
        }

        //
        // POST: /Employer/Edit
        [HttpPost]
        public ActionResult Edit(Employer employer)
        {
            if (ModelState.IsValid)
            {
                employerManager.SaveEmployer(employer);
                return RedirectToAction("Profile");
            }
            else
            {
                return View(employer);
            }
        }

        //
        // GET: /Employer/All

        public ActionResult All()
        {
            return View(employerManager.GetEmployers());
        }

        public ActionResult ReadEmployers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(employerManager.GetEmployers().ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UploadImage()
        {
            return PartialView("_UploadPartial");
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentType.StartsWith("image/"))
            {
                WebImage img = new WebImage(file.InputStream);
                if (img.Width > 178)
                {
                    img.Resize(178, img.Height);
                }

                if (img.Height > 178)
                {
                    img.Resize(img.Width, 178);
                }

                db.Employers.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Picture = img.GetBytes();
                db.SaveChanges();
            }
            return RedirectToAction("Profile");
        }
    }
}
