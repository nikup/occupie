using Occupie.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Occupie.Controllers
{
    public class BaseController : Controller
    {
        protected OccupieDb db = new OccupieDb();
        protected StudentManager studentManager = new StudentManager();
        protected EmployerManager employerManager = new EmployerManager();
        protected OfferManager offerManager = new OfferManager();
    }
}
