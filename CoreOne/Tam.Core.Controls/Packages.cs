using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tam.Core.Controls
{
    public static class Packages
    {
        private static Dictionary<string, HtmlString> Container = new Dictionary<string, HtmlString>();

        public static HtmlString Render(string packageName)
        {
            if (Container.ContainsKey(packageName))
            {
                return Container[packageName];
            }
            return HtmlString.Empty;
        }

        public static HtmlString AddScript(this IHtmlHelper helper, string packageName, params string[] filePaths)
        {
            string format = string.Format("<script src=\"{0}\"></script>");
            return AddFile(packageName, format, filePaths);
        }

        public static HtmlString AddStyle(this IHtmlHelper helper, string packageName, params string[] filePaths)
        {
            string format = string.Format("<link rel=\"stylesheet\" href=\"{0}\" />");
            return AddFile(packageName, format, filePaths);
        }

        private static HtmlString AddFile(string packageName, string format, string[] filePaths)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(format) && filePaths != null && filePaths.Any())
            {
                foreach (var item in filePaths)
                {
                    if (string.IsNullOrWhiteSpace(item) || item.EndsWith(".css", StringComparison.OrdinalIgnoreCase) == false ||
                        item.EndsWith(".js", StringComparison.OrdinalIgnoreCase) == false)
                    {
                        throw new Exception("File is invalid");
                    }
                    builder.AppendLine(string.Format(format, item));
                }
                var finalResult = new HtmlString(builder.ToString());
                Container.Add(packageName, finalResult);
                return finalResult;
            }
            return HtmlString.Empty;
        }
    }
}
