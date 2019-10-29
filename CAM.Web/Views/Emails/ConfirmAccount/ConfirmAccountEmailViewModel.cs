namespace CAM.Web.Views.Emails.ConfirmAccount
{
    public class ConfirmAccountEmailViewModel
    {
        public ConfirmAccountEmailViewModel(string url)
        {
            ConfirmAccountUrl = url;
        }
        public string ConfirmAccountUrl { get; set; }
    }
}