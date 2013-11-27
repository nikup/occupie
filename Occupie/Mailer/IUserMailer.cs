using Occupie.Enums;
using Mvc.Mailer;
using Occupie.Mailer;

namespace Occupie.Mailer
{ 
    public interface IUserMailer
    {
        MvcMailMessage Message(IMailModel model, EmailType type);
	}
}