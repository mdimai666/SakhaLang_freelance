using System;
using System.ComponentModel.DataAnnotations;

namespace AppShared.Models
{
    public class SysOptions
    {
        // General
        [Url]
        [Display(Name = "Адрес сайта")]
        public string SiteUrl { get; set; }
        [Display(Name = "Имя сайта")]
        public string SiteName { get; set; }
        [Display(Name = "Разрешить самостоятельную регистрацию")]
        public bool AllowUsersSelfRegister { get; set; }
        [Display(Name = "Роль по умолчанию для новых пользователей")]
        public Guid Default_Role { get; set; }

        //Specific
        public bool AutosetOrdersToCouriers { get; set; }
        public bool AllowAddZayavka { get; set; }
    }


}