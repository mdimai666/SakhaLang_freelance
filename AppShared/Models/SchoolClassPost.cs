using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("SchoolClassPost")]
    public class SchoolClassPost : Post
    {
        [Display(Name = "Класс")]
        [ForeignKey(nameof(AppShared.Models.SchoolClass))]
        public Guid SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }

        [ForeignKey(nameof(AppShared.Models.School))]
        [Display(Name = "Школа")]
        public Guid SchoolId { get; set; }
        public School School { get; set; }
    }
}