using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("LessonCategories")]
    public class LessonCategory : Post
    {
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}