using Newtonsoft.Json;

namespace CAM.Infrastructure.EmailTemplateData
{
    public class PasswordResetTemplateData
    {
        [JsonProperty("passResetUrl")]
        public string PassResetUrl { get; set; }
    }
}