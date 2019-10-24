namespace CAM.Infrastructure.Services
{
    /// <summary>
    /// Contains properties for storing SendGrid information to be used in the EmailSender service.
    /// </summary>
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}