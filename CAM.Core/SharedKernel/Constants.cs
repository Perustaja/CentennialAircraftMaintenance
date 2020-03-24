using System.Collections.Generic;

namespace CAM.Core.SharedKernel
{
    public class Constants
    {
        public const string DATE_FORMAT = "{0:MM/dd/yyyy}";
        public const decimal PRICE_MARKUP = 1.2m;
        public static IReadOnlyList<string> ALLOWED_IMAGE_EXTENSIONS = new List<string>() { ".png", ".jpg", "jpeg" };
        // This will store locally, and will be setup as a configuration variable in deployment
        public const string PARTS_IMAGES_DIRECTORY = "wwwroot/img/parts";
        public const string PARTS_THUMB_DIRECTORY = "wwwroot/img/parts/thumb";
        public const string PARTS_IMAGES_CONTENT_PATH = "~/img/parts";
        public const string PARTS_THUMB_CONTENT_PATH = "~/img/parts/thumb";
        public const string DEFAULT_IMAGE_PATH = PARTS_IMAGES_CONTENT_PATH + "/" + "default.png";
        public const string DEFAULT_THUMB_PATH = PARTS_THUMB_CONTENT_PATH + "/" + "default.png";
    }
}