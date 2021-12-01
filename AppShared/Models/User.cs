using AppShared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppShared.Models
{
    public class User : IdentityUser<Guid>, IBasicEntity
    {

        [ForeignKey(nameof(UserProfile))]
        public Guid ProfileId { get; set; }
        //public Profile Profile { get; set; }

        [PersonalData]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [PersonalData]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [PersonalData]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [PersonalData]
        [NotMapped]
        [Display(Name = "ФИО")]
        public string FullName => $"{MiddleName} {FirstName} {MiddleName}";

        [Display(Name = "День рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(AppShared.Models.City))]
        public Guid CityId { get; set; }
        [Display(Name = "Город")]
        public City City { get; set; }

        public decimal Balance { get; set; }
        public bool HasOnlinePay { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public EUserStatus Status { get; set; }

        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public override string ConcurrencyStamp { get; set; }
        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public override string PasswordHash { get; set; }
        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public override int AccessFailedCount { get; set; }
        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public override string NormalizedUserName { get; set; }
        [JsonIgnore, Newtonsoft.Json.JsonIgnore]
        public override string NormalizedEmail { get; set; }

        [EmailAddress]
        [Display(Name = "Почта для уведомлений")]
        public string EmailForNotify { get; set; }

        public string About { get; set; }

        public static User GetTest()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "example@mail.ru",
                BirthDate = new DateTime(1991, 2, 15),
                About = "About myself",
                UserName = "username1",
            };
        }
    }

    [Table("UserProfiles")]
    public class UserProfile : User
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        //public User User { get; set; }

        public string InstagramAccount { get; set; }



    }

    public enum EUserStatus
    {
        Disabled = 0,
        Created = 1,
        ProcessFillProfile = 2,
        PendingConfirm = 3,
        Activated = 10
    }

}