using System.Threading.Tasks;
using CAM.Core.Interfaces;
using CAM.Web.Interfaces;
using CAM.Web.Views.Emails.ConfirmAccount;

namespace CAM.Web.Services
{
    /// <summary>
    /// Generates the actual body to be returned to the ConfirmationEmailSender. This is separate because it requires AspNetCore.Mvc and only manipulates UI.
    /// It is not to be utilized as a standard injected service outside of ConfirmationEmailSender.
    /// </summary>
    public class ConfirmationEmailGenerator : IConfirmationEmailGenerator
    {
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        public ConfirmationEmailGenerator(IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }
        public async Task<string> GenerateConfirmationBody(string confirmationUrl)
        {
            return await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/ConfirmAccount.cshtml", 
                 new ConfirmAccountEmailViewModel(confirmationUrl));

        }
    }
}