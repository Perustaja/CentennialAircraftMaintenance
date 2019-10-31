using System.Threading.Tasks;
using CAM.Core.Interfaces;

namespace CAM.Infrastructure.Services
{
    /// <summary>
    /// Creates a structured confirmation email and then 
    /// </summary>
    public class ConfirmationEmailSender : IConfirmationEmailSender
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfirmationEmailGenerator _confirmationEmailGenerator;
        public ConfirmationEmailSender(IEmailSender emailSender, IConfirmationEmailGenerator confirmationEmailGenerator)
        {
            _emailSender = emailSender;
            _confirmationEmailGenerator = confirmationEmailGenerator;

        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            // Generate the message based on the confirmationUrl
            var message = await _confirmationEmailGenerator.GenerateConfirmationBody(confirmationUrl);
            // Pass the generatred message to the underlying EmailSender
            await _emailSender.SendEmailAsync(email, "Centennial Aircraft Maintenance", message);
        }
    }
}