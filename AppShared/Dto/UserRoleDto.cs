using AppShared.Models;
using AppShared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppShared.Dto
{
    public class UserRoleDto : User
    {
        [Display(Name = "Роли")]
        public IEnumerable<Role> Roles { get; set; }

        public UserRoleDto()
        {

        }

        public UserRoleDto(User user, IEnumerable<Role> userRoles)
        {
            Id = user.Id;
            ProfileId = user.ProfileId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            BirthDate = user.BirthDate;
            Email = user.Email;
            CityId = user.CityId;
            City = user.City;
            Balance = user.Balance;
            HasOnlinePay = user.HasOnlinePay;
            Created = user.Created;
            Modified = user.Modified;
            Status = user.Status;

            About = user.About;

            LockoutEnabled = user.LockoutEnabled;
            LockoutEnd = user.LockoutEnd;

            Roles = userRoles ?? new Role[] { };

        }
    }

    public class UserEditProfileDto
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Обо мне")]
        public string About { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [EmailAddress(ErrorMessageResourceName = nameof(AppRes.v_email), ErrorMessageResourceType = typeof(AppRes))]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [Display(Name = "ФИО")]
        public string FullName => $"{MiddleName} {FirstName} {MiddleName}";


        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [EmailAddress]
        [Display(Name = "Почта для уведомлений")]
        public string EmailForNotify { get; set; }

        public UserEditProfileDto()
        {

        }

        public UserEditProfileDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Email = user.Email;
            About = user.About;
            Phone = user.PhoneNumber;
            EmailForNotify = user.EmailForNotify;
        }
    }


    public class UserEditProfileExtraInfoForAdmin
    {


        //[StringLength(30, MinimumLength = 6, ErrorMessageResourceName = nameof(AppRes.v_range), ErrorMessageResourceType = typeof(AppRes))]
        //[Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        //[Display(Name = "Пароль")]
        //public string Password { get; set; }

        public IEnumerable<Guid> UserRoles { get; set; }

        /// <summary>
        /// Список ролей для выбора
        /// </summary>
        [Display(Name = "Роли")]
        public IEnumerable<Role> AwailRoles { get; set; }

        [Display(Name = "Статус")]
        public EUserStatus Status { get; set; }



        public UserEditProfileExtraInfoForAdmin()
        {

        }

    }
}
