using System.Text;
using Occupie.Enums;
using Mvc.Mailer;
using Occupie.Mailer;

namespace Occupie.Mailer
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_LayoutEmail";
		}

        public virtual MvcMailMessage Message(IMailModel model, EmailType type)
		{
			ViewData.Model = model;

			return Populate(x =>
			{
                x.BodyEncoding = Encoding.UTF8;
                x.IsBodyHtml = true;
				x.Subject = model.Subject;
                x.ViewName = type.ToString();
				x.To.Add(model.ReceiverEmail);
			});
		}


        //internal virtual MvcMailMessage AdminNotificationMessage()
        //{
        //    ViewData.Model = model;

        //    return Populate(x =>
        //    {
        //        x.BodyEncoding = Encoding.UTF8;
        //        x.IsBodyHtml = true;
        //        x.Subject = model.Subject;
        //        x.ViewName = "AdminMessageModel";
        //        x.To.Add(model.ReceiverEmail);
        //    });
        //}
    }
}