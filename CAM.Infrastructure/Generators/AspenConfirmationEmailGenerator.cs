using System.Text.Encodings.Web;
using CAM.Core.Interfaces;

namespace CAM.Infrastructure.Generators
{
    public class AspenConfirmationEmailGenerator : IEmailGenerator
    {
        public string ComposeSubject()
        {
            return "Centennial Aircraft Maintenance: Verify Email Address";
        }
        public string ComposeMessage(string callbackUrl)
        {
            return $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
        }

    }
}