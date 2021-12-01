using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppShared.Models
{
    public class EsiaUserInfo
    {
        public string firstName;
        public string lastName;
        public string middleName;
        public string birthDate;
        public string mobile;
        public string email;
        public bool trusted;
        public bool verifying;

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string Username => new string(mobile.Where(s => char.IsDigit(s) || s == '+').ToArray());
    }
}
