namespace LinkedFMI_UI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<LinkedFMI_UI.LinkedFMIDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LinkedFMI_UI.LinkedFMIDb context)
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
