using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CAM.Infrastructure.Services.DocGen.Helpers
{
    public static class RichText
    {
        public static XSSFRichTextString CreateRichTextString(string text, IFont font)
        {
            var richText = new XSSFRichTextString(text);
            richText.ApplyFont(font);
            return richText;
        }
    }
}