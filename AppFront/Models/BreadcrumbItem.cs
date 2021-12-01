
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components.Routing;

namespace AppFront.Models
{
    public class BreadcrumbItem
    {
        public string Text { get; set; }
        public string Url { get; set; }

        public BreadcrumbItem(string text, string url)
        {
            this.Text = text;
            this.Url = url;
        }

        public static List<BreadcrumbItem> ListFromString(string path)
        {
            return path.Split('|').Select(s =>
            {
                var ss = s.Split(':');
                return new BreadcrumbItem(
                    (ss.ElementAtOrDefault(1) ?? ss.ElementAtOrDefault(0) ?? "Text").Trim('/'),
                    ss.ElementAtOrDefault(0) ?? ""
                );
            }).ToList();
        }
    }
}