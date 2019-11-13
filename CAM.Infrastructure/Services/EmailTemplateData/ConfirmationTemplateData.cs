using Newtonsoft.Json;

namespace CAM.Infrastructure.Services.EmailTemplateData
{
    public class ConfirmationTemplateData
    {
        [JsonProperty("confirmUrl")]
        public string ConfirmUrl { get; set; }
    }
}