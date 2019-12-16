using Newtonsoft.Json;

namespace CAM.Infrastructure.EmailTemplateData
{
    public class ConfirmationTemplateData
    {
        [JsonProperty("confirmUrl")]
        public string ConfirmUrl { get; set; }
    }
}