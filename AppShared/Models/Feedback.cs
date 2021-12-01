using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("Feedbacks")]
    public class Feedback: Post
    {
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
