﻿using Occupie.Managers;
using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Occupie.Controllers
{
    public class TechnologyController : Controller
    {
        private StudentManager manager = new StudentManager();
        private OccupieDb db = new OccupieDb();

        [HttpPost]
        public void AddTechnology(Technology tech)
        {
            if (ModelState.IsValid)
            {
                var student = manager.GetStudentByUserId(WebSecurity.CurrentUserId);

                db.Technologies.Add(tech);
                db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Technologies.Add(tech);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public PartialViewResult RefreshTechnologies()
        {
            return PartialView("../Technology/_TechnologyPartial", manager.GetStudentByUserId(WebSecurity.CurrentUserId));
        }

        [HttpGet]
        public PartialViewResult AddTechnology()
        {
            return PartialView("_AddTechPartial", new Technology());
        }

        [HttpGet]
        public PartialViewResult DeleteTechnology(int techId, int listId)
        {
            return PartialView("_DelTechPartial", new Int32[] { techId, listId });
        }

        [HttpPost]
        public void DeleteTechnology(int techId)
        {
            db.Students.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId).Technologies.RemoveAll(x => x.TechId == techId);
            db.SaveChanges();
        }
    }
}
