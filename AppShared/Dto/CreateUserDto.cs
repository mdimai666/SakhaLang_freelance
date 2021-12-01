using AppShared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppShared.Dto
{
    public class CreateUserDto
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [EmailAddress(ErrorMessageResourceName = nameof(AppRes.v_email), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 6, ErrorMessageResourceName = nameof(AppRes.v_range), ErrorMessageResourceType = typeof(AppRes))]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Роли")]
        public IEnumerable<Guid> Roles { get; set; }

    }
}
