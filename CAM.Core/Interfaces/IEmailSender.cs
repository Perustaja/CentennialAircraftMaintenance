using System.Threading.Tasks;

namespace CAM.Core.Interfaces
{

    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string message);
        Task SendConfirmationEmail(string email, string confirmationUrl);
        Task SendPasswordResetEmail(string email, string passResetUrl);
    }
}