using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("ActionHistory")]
    public class ActionHistory : BasicEntity
    {
        [Display(Name = "Заголовок")]
        [StringLength(500)]
        public string Title { get; set; }
        
        [Display(Name = "Содержимое")]
        public string Content { get; set; }
        
        [Display(Name = "Модель")]
        [StringLength(200)]
        public string ActionModel { get; set; }

        [Display(Name = "Сообщение")]
        [StringLength(1000)]
        public string Message { get; set; }
        [Display(Name = "Уровень")]
        public EActionLevel ActionLevel { get; set; }

    }

    public enum EActionLevel
    {
        None = 0,
        Info = 10,
        Warn = 20,
        Error = 30,
    }
}