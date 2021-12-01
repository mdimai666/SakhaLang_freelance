using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("SchoolPosts")]
    public class SchoolPost : Post
    {
        [ForeignKey(nameof(AppShared.Models.School))]
        [Display(Name = "Школа")]
        public Guid SchoolId { get; set; }
        public School School { get; set; }
    }
}