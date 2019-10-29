namespace CAM.Core.Options
{
    /// <summary>
    /// Contains properties for storing SendGrid information to be used in the EmailSender service. 
    /// This is stored in usersecrets in development and should be stored in configuration in production.
    /// </summary>
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}