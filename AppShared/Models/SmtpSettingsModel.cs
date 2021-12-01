using System.ComponentModel.DataAnnotations;

namespace AppShared.Models
{
    public class SmtpSettingsModel
    {
        [Required, Display(Name = "Почтовый сервер")]
        public string Host { get; set; }
        [Required, Display(Name = "Порт")]
        public int Port { get; set; }
        [Required, Display(Name = "Логин")]
        public string SmtpUser { get; set; }
        [Required, Display(Name = "Пароль")]
        public string SmtpPassword { get; set; }
        [Display(Name = "Требуется SSL")]
        public bool Secured { get; set; } = true;

        [Required, Display(Name = "От имени")]
        public string FromName { get; set; }
        [Display(Name = "Тестовый сервер. Не отправлять уведомления")]
        public bool IsTestServer { get; set; }

    }
    public class TestMailMessage
    {
        [Required, Display(Name = "Кому. Почта.")]
        public string Email { get; set; }
        [Required, Display(Name = "Тема")]
        public string Subject { get; set; }
        [Required, Display(Name = "Сообщение")]
        public string Message { get; set; }
        
    }
}