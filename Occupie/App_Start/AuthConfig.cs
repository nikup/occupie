using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using Occupie.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace Occupie
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
         //   WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

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
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();

            OAuthWebSecurity.RegisterLinkedInClient("9ei6xj5uz86j", "7Z9pJAxozvG7B6vP", "LinkedIn");
        }
    }
}
