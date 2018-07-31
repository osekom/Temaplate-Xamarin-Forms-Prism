using System;
namespace TemplateSpartaneApp.Helpers
{
    public static class HTMLHelper
    {
        public static string GetHTMLContentFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            var htmlDecode = System.Net.WebUtility.HtmlDecode(text);

            return htmlDecode;
        }
    }
}
