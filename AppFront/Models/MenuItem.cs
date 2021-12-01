
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.Generic;

namespace AppFront
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }


        public bool SubItemFlag { get; set; }

        public List<MenuItem> SubItems {get;set;}

        public NavLinkMatch navLinkMatch { get; set; } = NavLinkMatch.Prefix;

        public MenuType menuType { get; set; }

        public enum MenuType
        {
            Link,
            Header
        }

        public string Role { get; set; }
    }
}