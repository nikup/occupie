using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkedFMI_UI.Models;
using LinkedFMI_UI.Managers;
using WebMatrix.WebData;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LinkedFMI_UI.ViewModels;
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

        public ActionResult Index()
        {
            return View(GetProducts());
        }

        public ActionResult ReadEmployers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetProducts().ToDataSourceResult(request));
        }

        private List<EmployerAllViewModel> GetProducts()
        {
            var employers = new List<EmployerAllViewModel>();

            foreach (var empl in db.Employers)
            {
                employers.Add(new EmployerAllViewModel
                    {
                        Id = empl.EmployerProfileId,
                        Name = empl.Name,
                        Picture = "data:image;base64," + System.Convert.ToBase64String(empl.Picture)
                    });
            }

            return employers;
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
