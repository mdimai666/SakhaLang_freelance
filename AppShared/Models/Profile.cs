using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    /// <summary>
    /// User profile class
    /// </summary>
    public class Profile
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
}
