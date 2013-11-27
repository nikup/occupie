using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Occupie.Mailer
{
    public interface IMailModel
    {
        int ReceiverUserId { get; set; }
        string ReceiverEmail { get; set; }
        string Subject { get; set; }
    }
}