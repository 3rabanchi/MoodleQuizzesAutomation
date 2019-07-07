using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;

namespace TeachingTool.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("teachingtoolpw@gmail.com", "Teaching Tool"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
        //public task sendemailasync(string email, string subject, string htmlmessage)
        //{
        //    var client = new smtpclient("smtp.gmail.com", 587)
        //    {
        //        usedefaultcredentials = false,
        //        credentials = new networkcredential("sultan.aljanabi@gmail.com", "s6u5l4t3a2n1"),
        //        enablessl = true,
        //    };
        //    var mailmessage = new mailmessage
        //    {
        //        from = new mailaddress("sultan.aljanabi@gmail.com")
        //    };
        //    mailmessage.to.add(email);
        //    mailmessage.subject = subject;
        //    mailmessage.body = htmlmessage;
        //    return client.sendmailasync(mailmessage);
        //}
    }
}
