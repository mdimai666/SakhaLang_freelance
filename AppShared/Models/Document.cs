using AppShared.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("Documents")]
    public class Document : BasicEntity
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [DisplayName("Название документа")]
        public string Title { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [ForeignKey(nameof(AppShared.Models.FileEntity))]
        public Guid FileId { get; set; }
        [DisplayName("Файл")]
        public FileEntity File { get; set; }
    }



}