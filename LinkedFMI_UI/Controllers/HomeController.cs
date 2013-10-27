using Kendo.Mvc.UI;
using LinkedFMI_UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LinkedFMI_UI.ViewModels;
using System.Web.Helpers;
using LinkedFMI_UI.Models;

namespace LinkedFMI_UI.Controllers
{
    public class HomeController : Controller
    {
        private LinkedFMIDb db = new LinkedFMIDb();

        private const int NumberOfItemsToShow = 5;
        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            if (db.Offers.Count() < NumberOfItemsToShow)
                viewModel.Offers = db.Offers;
            else
                viewModel.Offers = db.Offers.OrderBy(x => x.OfferId).Skip(db.Offers.Count() - NumberOfItemsToShow);

            if (db.Students.Count() < NumberOfItemsToShow)
                viewModel.Students = GetStudents();
            else
                viewModel.Students = GetStudents().OrderBy(x => x.Id).Skip(db.Students.Count() - NumberOfItemsToShow);

            if (db.Employers.Count() < NumberOfItemsToShow)
                viewModel.Employers = GetEmployers();
            else
                viewModel.Employers = GetEmployers().OrderBy(x=> x.Id).Skip(db.Employers.Count() - NumberOfItemsToShow);
            return View(viewModel);
        }

        public ActionResult ReadStudents([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetStudents().ToDataSourceResult(request));
        }

        public ActionResult ReadEmployers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetEmployers().ToDataSourceResult(request));
        }

        private List<EmployerAllViewModel> GetEmployers()
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

        private List<StudentAllViewModel> GetStudents()
        {
            var students = new List<StudentAllViewModel>();

            foreach (var empl in db.Students)
            {
                students.Add(new StudentAllViewModel
                {
                        Id= empl.StudentProfileId,
                        FullName = empl.FirstName + " " + empl.LastName,
                        PictureString = "data:image;base64," + System.Convert.ToBase64String(empl.Picture)
                });
            }

            return students;
        }

        public ActionResult Student()
        {
            return View();
        }

        public ActionResult Employer()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult LatestOffers()
        {
            return PartialView("_LatestOffersPartial");
        }
    }
}
