using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    //[Table("PlaylistLessons")]
    //public class PlaylistLesson
    //{
    //    [Column(Order = 0)]
    //    //[ForeignKey(nameof(LessonCourseId))]
    //    public Guid LessonCourseId { get; set; }
    //    //[Key]
    //    [Column(Order = 1)]
    //    //[ForeignKey(nameof(LessonId))]
    //    public Guid LessonId { get; set; }

    //    public virtual Playlist LessonCourse { get; set; }
    //    public virtual Lesson Lesson { get; set; }
    //}

    [Table("School")]
    public class School : Post
    {
        public Director Director { get; set; }
        [Display(Name = "Классы")]
        public virtual ICollection<SchoolClass> SchoolClasses { get; set; }
        [Display(Name = "Посты")]
        public virtual ICollection<SchoolPost> SchoolPosts { get; set; }
        [Display(Name = "Уроки")]
        //public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        [Display(Name = "Курсы")]
        public virtual ICollection<Playlist> LessonCourses { get; set; }

        public static readonly Guid DefaultSchoolId = Guid.Parse("2b5c6d66-c8a4-4253-b983-6e2435c49049");
    }

    [Owned]
    public class Director
    {
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Эл. почта")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Телефон")]
        [Phone]
        public string Phone { get; set; }
    }
}