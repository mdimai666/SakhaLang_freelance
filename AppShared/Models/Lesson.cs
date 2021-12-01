using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace AppShared.Models
{

    [Table("Lessons")]
    public class Lesson : Post
    {
        [Display(Name = "Ссылка на видео")]
        public string VideoLink { get; set; }
        [Display(Name = "Плейлист")]
        public virtual ICollection<Playlist> Playlists { get; set; }

        [Display(Name = "Школа")]
        [ForeignKey(nameof(AppShared.Models.School))]
        public Guid SchoolId { get; set; }
        public School School { get; set; }
        //public Category Category { get; set; }
        //[ForeignKey(nameof(AppShared.Models.Category))]
        //public Guid CategotyId { get; set; }

        [Display(Name = "Категория")]
        [ForeignKey(nameof(AppShared.Models.LessonCategory))]
        public override Guid CategoryId { get; set; }
        public LessonCategory Category { get; set; }
    }

    
}