using AppShared.Resources;
using System.ComponentModel.DataAnnotations;

namespace AppShared.Dto
{
    public class FeedbackDto
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Текст сообщения")]
        public string TextMessage { get; set; }
    }
}
