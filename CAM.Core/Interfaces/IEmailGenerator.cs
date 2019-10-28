namespace CAM.Core.Interfaces
{
    public interface IEmailGenerator
    {
        string ComposeSubject();
        string ComposeMessage(string callbackUrl);
    }
}