namespace Occupie.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<Occupie.OccupieDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Occupie.OccupieDb context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
       "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Student"))
            {
                roles.CreateRole("Student");
            }

            if (!roles.RoleExists("Employer"))
            {
                roles.CreateRole("Employer");
            }
        }
    }
}
