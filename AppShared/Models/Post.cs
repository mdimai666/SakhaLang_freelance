using AppShared.Interfaces;
using AppShared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Serializable]
    [Table("posts")]
    public class Post : BasicEntity, IPost, IEntityFilesSupport
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public virtual string Slug { get; set; } = Guid.NewGuid().ToString();
        //[NotMapped]
        //public override string Value => Content;
        [Display(Description = "Теги")]
        public virtual List<string> Tags { get; set; }
        public virtual Guid ParentId { get; set; }
        public virtual Guid CategoryId { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Заголовок")]
        public virtual string Title { get; set; }
        [Display(Name = "Текст")]
        public virtual string Content { get; set; }
        [Display(Name = "Кратко")]
        public virtual string Excerpt { get; set; }
        [Display(Name = "Изображение")]
        public virtual string Image { get; set; }
        [Display(Name = "Статус")]
        public virtual EPostStatus Status { get; set; }
        [Display(Name = "Тип")]
        public virtual string Type { get; set; }

        //public static readonly string[] StatusList = { "draft", "pending", "private", "publish", "future", "trash", "auto-draft", "inherit" };

        public static readonly string[] TypeList = { "post", "page", "attachment" };

        public static readonly EPostStatus defaultStatus = EPostStatus.Draft;
        public static readonly string defaultType = "post";

        [Display(Name = "Файл")]
        public virtual ICollection<FileEntity> FileList {  get; set; }
        //public virtual ICollection<PostFiles> FileList {  get; set; }
    }

    //public class PostFiles
    //{
    //    [ForeignKey(nameof(AppShared.Models.Post))]
    //    public Guid PostId { get; set; }
    //    public Post Post { get; set; }
    //    [ForeignKey(nameof(AppShared.Models.FileEntity))]
    //    public Guid FileId { get; set; }
    //    public virtual FileEntity File { get; set; }
    //}

    

    
}