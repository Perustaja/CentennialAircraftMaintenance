using CAM.Core.Interfaces;
using CAM.Core.Options;
using CAM.Infrastructure.EmailTemplateData;
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

        public Task SendEmail(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }
        public Task SendConfirmationEmail(string email, string confirmationUrl)
        {
            return ExecuteConfirmation(Options.SendGridKey, email, confirmationUrl);
        }
        public Task SendPasswordResetEmail(string email, string passResetUrl)
        {
            return ExecutePasswordReset(Options.SendGridKey, email, passResetUrl);
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
        /// Utilizes transactional templates and the SendGrid dynamic content API to create/send an account 
        /// confirmation url.
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
        /// <summary>
        /// Utilizes transactional templates and the SendGrid dynamic content API to create/send
        /// a password reset email.
        /// </summary>
        public Task ExecutePasswordReset(string apiKey, string email, string passResetUrl)
        {
            var dynamicTemplateData = new PasswordResetTemplateData()
            {
                PassResetUrl = passResetUrl
            };

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                TemplateId = "d-b42fcf5f2418464fb32a1178fdd53ad3",
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