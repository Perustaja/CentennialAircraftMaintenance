using Newtonsoft.Json;

namespace CAM.Infrastructure.Services.EmailTemplateData
{
    public class PasswordResetTemplateData
    {
        [JsonProperty("passResetUrl")]
        public string PassResetUrl { get; set; }
    }
}