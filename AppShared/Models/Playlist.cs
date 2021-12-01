using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("Playlists")]
    public class Playlist : Post
    {
        [Display(Name = "Уроки")]
        public virtual ICollection<Lesson> Lessons { get; set; }

        [Display(Name = "Школа")]
        [ForeignKey(nameof(AppShared.Models.School))]
        public Guid SchoolId { get; set; }
        public School School { get; set; }

        [Display(Name = "Категория")]
        [ForeignKey(nameof(AppShared.Models.LessonCategory))]
        public override Guid CategoryId { get; set; }
        public LessonCategory Category { get; set; }

    }
}