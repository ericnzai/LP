using System;
using System.Web;

namespace LP.Model.Extensions
{
    public static class StringExtensions
    {
        public static string ToGroupUrl(this string groupFriendlyUrl, string trainingAreaFriendlyUrl)
        {
            return string.Format("{0}/{1}/Chapters.aspx", trainingAreaFriendlyUrl, groupFriendlyUrl);
        }

        public static string StripHtmlTags(this string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            return HttpUtility.HtmlDecode(doc.DocumentNode.InnerText);
        }

        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length) return input;
            var nextSpace = input.LastIndexOf(" ", length, System.StringComparison.Ordinal);
            return string.Format("{0}...", input.Substring(0, (nextSpace > 0) ? nextSpace : length).Trim());
        }

       
    }
}
