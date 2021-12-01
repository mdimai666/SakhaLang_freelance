
using System.ComponentModel.DataAnnotations;

namespace AppShared.Dto
{
    public class AuthCreditionalsDto
    {

        [Required(ErrorMessage = "Заполните Логин/Почту")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Заполните Пароль")]
        public string Password { get; set; }
    }

}