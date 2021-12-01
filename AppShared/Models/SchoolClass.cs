using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("SchoolClasses")]
    public class SchoolClass : Post
    {
        [Display(Name = "Школа")]
        [ForeignKey(nameof(AppShared.Models.School))]
        public Guid SchoolId { get; set; }
        public School School { get; set; }
        //[Display(Name = "Название")]
        //public string Title { get; set; }
        //[Display(Name = "Описание")]
        //public string Content { get; set; }
        //[Display(Name = "Изображение")]
        //public string Image { get; set; }
        //[Display(Name = "Статус")]
        //public EPostStatus Status { get; set; }

        public virtual ICollection<SchoolClassPost> SchoolClassPosts { get; set; }
    }
}