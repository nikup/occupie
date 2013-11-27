using Kendo.Mvc.UI;
using Occupie.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using System.Web.Helpers;
using Occupie.Models;
using Occupie.Managers;

namespace Occupie.Controllers
{
    public class HomeController : Controller
    {
        private const int NumberOfItemsToShow = 5;

        private OccupieDb db = new OccupieDb();
        private EmployerManager empManager = new EmployerManager();
        private StudentManager studentManager = new StudentManager();

        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();

            if (db.Offers.Count() < NumberOfItemsToShow)
            {
                viewModel.Offers = db.Offers;
            }
            else
            {
                viewModel.Offers = db.Offers.OrderBy(x => x.OfferId).Skip(db.Offers.Count() - NumberOfItemsToShow);
            }

            if (db.Students.Count() < NumberOfItemsToShow)
            {
                viewModel.Students = studentManager.GetStudents();
            }
            else
            {
                viewModel.Students = studentManager.GetStudents().OrderBy(x => x.Id).Skip(db.Students.Count() - NumberOfItemsToShow);
            }

            if (db.Employers.Count() < NumberOfItemsToShow)
            {
                viewModel.Employers = empManager.GetEmployers();
            }
            else
            {
                viewModel.Employers = empManager.GetEmployers().OrderBy(x => x.Id).Skip(db.Employers.Count() - NumberOfItemsToShow);
            }

            return View(viewModel);
        }

        public ActionResult ReadStudents([DataSourceRequest] DataSourceRequest request)
        {
            return Json(studentManager.GetStudents().ToDataSourceResult(request));
        }

        public ActionResult ReadEmployers([DataSourceRequest] DataSourceRequest request)
        {
            return Json(empManager.GetEmployers().ToDataSourceResult(request));
        }

        [HttpGet]
        public PartialViewResult LatestOffers()
        {
            return PartialView("_LatestOffersPartial");
        }
    }
}