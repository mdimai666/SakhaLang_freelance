using AppShared.Interfaces;
using AppShared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("Cities")]
    public class City : IBasicEntity, IActivatable
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public virtual DateTime Modified { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Currency { get; set; }
        /// <summary>
        /// Lan,Lat = 62,03223,129.2323
        /// </summary>
        public string Location { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [MinLength(3)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string CurrencySymbol { get; set; }

        [NotMapped]
        public string Title => Name;


        [NotMapped]
        public Guid UserId { get; set; }
        [NotMapped]
        public User User { get; set; }

        public bool Active { get; set; }

    }
}