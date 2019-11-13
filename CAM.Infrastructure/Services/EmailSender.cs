using CAM.Core.Interfaces;
using CAM.Core.Options;
using CAM.Infrastructure.Services.EmailTemplateData;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CAM.Infrastructure.Services
{
    /// <summary>
    /// Sends emails via SendGrid using AuthMessageSenderOptions.
    /// </summary>
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
        public Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            return ExecuteConfirmation(Options.SendGridKey, email, confirmationUrl);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@aspenmaintenance.dev", Options.SendGridUser),
                Subject = subject,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
        /// <summary>
        /// Utilizes transactional templates and the SendGrid dynamic content API to create the end email.
        /// </summary>
        public Task ExecuteConfirmation(string apiKey, string email, string confirmationUrl)
        {
            var dynamicTemplateData = new ConfirmationTemplateData()
            {
                ConfirmUrl = confirmationUrl
            };

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                TemplateId = "d-beb3599e0c3f4ab1b63592d4b7985527",
                From = new EmailAddress("no-reply@aspenmaintenance.dev", Options.SendGridUser),
            };
            msg.SetTemplateData(dynamicTemplateData);
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

    }
}