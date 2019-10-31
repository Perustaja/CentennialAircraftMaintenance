using System.Threading.Tasks;

namespace CAM.Core.Interfaces
{
    public interface IConfirmationEmailGenerator
    {
        Task<string> GenerateConfirmationBody(string confirmationUrl);
    }
}