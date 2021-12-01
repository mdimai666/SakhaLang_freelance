using AppShared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{

    [Table("Contest")]
    public class Contest : BasicEntity
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }
        [Display(Name = "Дата завершения")]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Range(0, 1_000_000_000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Бюджет")]
        public decimal Budget { get; set; }

        [Display(Name = "Заявки")]
        public virtual ICollection<Post> PostList { get; set; }

        [Display(Name = "Завершен")]
        public bool Completed { get; set; }
    }

}
