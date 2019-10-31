namespace CAM.Web.Views.Emails.ConfirmAccount
{
    public class ConfirmAccountEmailViewModel
    {
        public ConfirmAccountEmailViewModel(string confirmUrl)
        {
            ConfirmAccountUrl= confirmUrl;
        }
        public string ConfirmAccountUrl { get; set; }

    }
}