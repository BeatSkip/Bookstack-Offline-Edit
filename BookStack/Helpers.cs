using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStackApiClient;
using VsCodeStack.Configuration;


namespace VsCodeStack.BookStack
{
    public static class Helpers
    {
        public static string BookStackBrowser(string host, string token, string secret)
        {
            throw new NotImplementedException("Not implemented");
        }

        public static string UrlToSlug(string url)
        {
            var slug = url.Substring(url.LastIndexOf('/') + 1);
            return slug;

        }

        public static string GetChapterFolder(string top, ChapterItem chapter)
        {
            var folderName = $"{chapter.priority.ToString("D2")} - {chapter.name}";
            return System.IO.Path.Combine(top, folderName);
        }
    }
}
