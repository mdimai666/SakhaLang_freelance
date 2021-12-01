using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AppShared.Models
{
    public class Comment: BasicEntity
    {
        public string MessageHtml { get; set; }

        [ForeignKey(nameof(AppShared.Models.Post))]
        public Guid PostId { get; set; }


        [NotMapped]
        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public string PlainText => StripHTML(MessageHtml);

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
