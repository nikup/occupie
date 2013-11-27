using Occupie.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Occupie
{
    public class OccupieDb : DbContext
    {
        public OccupieDb()
            :base("DefaultConnection")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<FMIEdu> FMIEdus { get; set; }
        public DbSet<Bachelor> Bachelors { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}