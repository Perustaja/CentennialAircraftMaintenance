using System.Collections.Generic;

namespace CAM.Core.SharedKernel
{
    public class Constants
    {
        public const string DATE_FORMAT = "{0:MM/dd/yyyy}";
        public const decimal PRICE_MARKUP = 1.2m;
        // This will store locally, and will be setup as a configuration variable in deployment
        public const string PARTS_DIRECTORY = "wwwroot/img/parts";
        public const string DEFAULT_IMAGE_NAME = "default.png";
    }
}