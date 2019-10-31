using System.Threading.Tasks;

namespace CAM.Core.Interfaces
{

    public interface IConfirmationEmailSender
    {
        Task SendConfirmationEmailAsync(string email, string confirmationUrl);
    }
}