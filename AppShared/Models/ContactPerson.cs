using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("ContactPersons")]
    public class ContactPerson : BasicEntity
    {
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Должность")]
        public string Function { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Описание")]
        [MaxLength(250)]
        public string Description { get; set; }

    }
}
