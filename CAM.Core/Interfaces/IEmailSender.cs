using System.Threading.Tasks;

namespace CAM.Core.Interfaces
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendConfirmationEmailAsync(string email, string confirmationUrl);
    }
}