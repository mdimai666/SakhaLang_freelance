using AppShared.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    /// <summary>
    /// Населенный пункт
    /// </summary>
    [Display(Name = "Населенный пункт")]
    public class GeoLocation : BasicEntityNonUser
    {
        //public int LocId { get; set; }
        [ForeignKey(nameof(AppShared.Models.GeoMunicipality))]
        public Guid GeoMunicipalityId { get; set; }
        public virtual GeoMunicipality GeoMunicipality { get; set; }

        [Display(Name = "Код ОКТМО")]
        [MaxLength(50)]
        public string OKTMO { get; set; }
        /// <summary>
        /// Официальное название населенного пункта
        /// </summary>
        [Display(Name = "Официальное название населенного пункта")]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// Краткое название населенного пункта
        /// </summary>
        [Display(Name = "Краткое название населенного пункта")]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [MaxLength(150)]
        public string ShortName { get; set; }
        /// <summary>
        /// Численность населения
        /// </summary>
        [Display(Name = "Численность населения")]
        public int LocPopulation { get; set; }
        public bool Active { get; set; }
        //public byte[] Tm { get; set; }

        [Display(Name = "Тип населенного пункта")]
        [ForeignKey(nameof(AppShared.Models.GeoLocationType))]
        public Guid GeoLocationTypeId { get; set; }
        public GeoLocationType GeoLocationType { get; set; }
        [Display(Name = "Широта")]
        [MaxLength(50)]
        public string Latitude { get; set; }
        [MaxLength(50)]
        [Display(Name = "Долгота")]
        public string Longitude { get; set; }

        public virtual ICollection<GeoRegionCenter> GeoRegionCenters { get; set; }
        //public virtual ICollection<Meeting> Meetings { get; set; }
    }


    /// <summary>
    /// Тип населенного пункта
    /// </summary>
    [Display(Name = "Тип населенного пункта")]
    [Table("GeoLocationTypes")]
    public class GeoLocationType : BasicEntityNonUser
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        //public int LocTypeId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Название")]
        public string Title { get; set; }
    }

    /// <summary>
    /// Данные администрации поселения
    /// </summary>
    [Owned]
    //[Table("GeoMunicInfo")]
    public class GeoMunicInfo
    {
        ////public int MunicInfoId { get; set; }
        //[ForeignKey(nameof(AppShared.Models.GeoMunicipality))]
        //public Guid GeoMunicipalityId { get; set; }
        //public virtual GeoMunicipality GeoMunicipality { get; set; }

        /// <summary>
        /// Имя главы поселения
        /// </summary>
        [Display(Name = "Имя главы поселения")]
        [MaxLength(150)]
        public string AdminHeadName { get; set; }
        /// <summary>
        /// Телефон администрации поселения. Стационарный
        /// </summary>
        [Display(Name = "Телефон администрации поселения")]
        [MaxLength(50)]
        public string AdminHeadFixedPhone { get; set; }
        /// <summary>
        /// Телефон главы поселения (мобильный)
        /// </summary>
        [Display(Name = "Телефон главы поселения (мобильный)")]
        [MaxLength(50)]
        public string AdminHeadMobilePhone { get; set; }
        /// <summary>
        /// Эл.почта администрации поселения
        /// </summary>
        [Display(Name = "Эл.почта администрации поселения")]
        [MaxLength(100)]
        public string AdminHeadEmail { get; set; }
        /// <summary>
        /// Почтовый адрес администрации поселения
        /// </summary>
        [Display(Name = "Почтовый адрес администрации поселения")]
        [MaxLength(250)]
        public string AdminPostAddress { get; set; }
        public bool Active { get; set; }
        //public byte[] Tm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool ContainsRegCenter { get; set; }

    }

    /// <summary>
    /// Поселения
    /// </summary>
    [Display(Name = "Поселения")]
    [Table("GeoMunicipality")]
    public class GeoMunicipality : BasicEntityNonUser
    {
        /// <summary>
        /// Муниципальный район
        /// </summary>
        [Display(Name = "Муниципальный район")]
        //public int MunicId { get; set; }
        public Guid GeoRegionId { get; set; }
        public virtual GeoRegion Reg { get; set; }

        [Display(Name = "Тип поселения")]
        [ForeignKey(nameof(AppShared.Models.GeoMunicType))]
        public Guid MunicTypeId { get; set; }
        public virtual GeoMunicType MunicType { get; set; }

        /// <summary>
        /// Официальное название поселения
        /// </summary>
        [Display(Name = "Официальное название поселения")]
        [MaxLength(200)]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Name { get; set; }
        /// <summary>
        /// Краткое название поселения
        /// </summary>
        [Display(Name = "Краткое название поселения")]
        [MaxLength(150)]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string ShortName { get; set; }

        /// <summary>
        /// Численность населения
        /// </summary>
        [Display(Name = "Численность населения")]
        public int? Population { get; set; }
        [Display(Name = "Код ОКТМО")]
        [MaxLength(50)]
        public string OKTMO { get; set; }
        [Display(Name = "Код OKATO")]
        [MaxLength(50)]
        public string OKATO { get; set; }
        //public string RegOCTMO { get; set; }
        //public int? MasterId { get; set; }
        //public DateTime? MergeDate { get; set; }
        public bool Active { get; set; }
        //public byte[] Tm { get; set; } 

        //public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<GeoLocation> GeoLocations { get; set; }

        /// <summary>
        /// Данные администрации поселения
        /// </summary>
        [Display(Name = "Данные администрации поселения")]
        //[Owned]
        //[ForeignKey(nameof(AppShared.Models.GeoMunicInfo))]
        //public Guid GeoMunicInfoId { get; set; }
        public virtual GeoMunicInfo GeoMunicInfo { get; set; } = new();
        //public virtual ICollection<Meeting> Meetings { get; set; }
    }

    /// <summary>
    /// Тип поселения
    /// </summary>
    [Display(Name = "Тип поселения")]
    [Table("GeoMunicTypes")]
    public class GeoMunicType : BasicEntityNonUser
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [MaxLength(50)]
        //public int MunTypeId { get; set; }
        [Display(Name = "Название")]
        public string Title { get; set; }
        //public string MunTypeNameE { get; set; }

        public virtual ICollection<GeoMunicipality> GeoMunicipalities { get; set; }
        //public virtual ICollection<ProgramDocMunicType> ProgramDocMunicTypes { get; set; }
    }

    /// <summary>
    /// Район
    /// </summary>
    [Display(Name = "Район")]
    [Table("GeoRegions")]
    public class GeoRegion : BasicEntityNonUser
    {
        //public int RegId { get; set; }
        /// <summary>
        /// Официальное название района
        /// </summary>
        [Display(Name = "Официальное название района")]
        [MaxLength(200)]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Name { get; set; }
        /// <summary>
        /// Краткое название района
        /// </summary>
        [Display(Name = "Краткое название района")]
        [MaxLength(150)]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string ShortName { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        [MaxLength(50)]
        [Display(Name = "Код ОКТМО")]
        public string OKTMO { get; set; }
        /// <summary>
        /// До 2014 года использовался. Легаси
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "Код ОКАТО")]
        public string OKATO { get; set; }
        [Display(Name = "Численность населения")]
        public int Population { get; set; }
        [MaxLength(50)]
        public string RegOktmo { get; set; }
        public bool Active { get; set; }
        public RegionType RegType { get; set; }
        //public byte[] Tm { get; set; }
        /// <summary>
        /// Регион является северным
        /// </summary>
        [Display(Name = "Регион является северным")]
        public bool RegIsNorthern { get; set; }

        //public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<GeoMunicipality> GeoMunicipalities { get; set; }
        public virtual ICollection<GeoRegionCenter> GeoRegionCenters { get; set; }
        //public virtual ICollection<GeoRegionHistory> GeoRegionHistories { get; set; }

        /// <summary>
        /// Данные администрации района
        /// </summary>
        public virtual GeoRegionInfo GeoRegionInfo { get; set; } = new();
        //public virtual ICollection<Meeting> Meetings { get; set; }
    }

    public enum RegionType
    {
        Unknown = 0,
        /// <summary>
        /// Муниципальный
        /// </summary>
        Municipal,
        /// <summary>
        /// Городской
        /// </summary>
        Urban
    }

    [Table("GeoRegionCenter")]
    public class GeoRegionCenter : BasicEntityNonUser
    {
        //public int RegCenterId { get; set; }
        [ForeignKey(nameof(AppShared.Models.GeoRegion))]
        public Guid GeoRegionId { get; set; }
        public virtual GeoRegion GeoRegion { get; set; }

        [ForeignKey(nameof(AppShared.Models.GeoLocation))]
        public Guid GeoLocationId { get; set; }
        public virtual GeoLocation GeoLocation { get; set; }

    }

    /// <summary>
    /// Данные администрации района
    /// </summary>
    //[Table("GeoRegionInfo")]
    [Owned]
    public class GeoRegionInfo
    {
        //public int RegInfoId { get; set; }

        //[ForeignKey(nameof(AppShared.Models.GeoRegion))]
        //public Guid GeoRegionId { get; set; }
        //public virtual GeoRegion GeoRegion { get; set; }
        /// <summary>
        /// Имя главы администрации района
        /// </summary>
        [Display(Name = "Имя главы администрации района")]
        [MaxLength(150)]
        public string HeadName { get; set; }
        /// <summary>
        /// Номер телефона районной администрации
        /// </summary>
        [Display(Name = "Номер телефона районной администрации")]
        [MaxLength(50)]
        public string HeadPhone { get; set; }
        /// <summary>
        /// Адрес эл.почты районной администрации
        /// </summary>
        [Display(Name = "Адрес эл.почты районной администрации")]
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// Почтовый адрес администрации района
        /// </summary>
        [Display(Name = "Почтовый адрес администрации района")]
        [MaxLength(250)]
        public string PostAddress { get; set; }
        /// <summary>
        /// Имя районного куратора ППМИ
        /// </summary>
        [Display(Name = "Имя районного куратора ППМИ")]
        [MaxLength(150)]
        public string AssistantName { get; set; }
        /// <summary>
        /// Мобильный телефон куратора
        /// </summary>
        [Display(Name = "Мобильный телефон куратора")]
        [MaxLength(50)]
        public string AssistantPhone { get; set; }
        /// <summary>
        /// Адрес эл.почты куратора
        /// </summary>
        [Display(Name = "Адрес эл.почты куратора")]
        [MaxLength(100)]
        public string AssistantEmail { get; set; }
        public bool Active { get; set; }
        //public byte[] Tm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }
}