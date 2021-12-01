using AppShared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Serializable]
    [Table("meta")]
    public sealed class Meta : SimpleEntity
    {
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Key { get; set; }
        //[DataType(DataType.DateTime)]
        //public DateTime Modified { get; set; }

        public static readonly string defaultStatus = "";
        public static readonly string defaultType = "";

        public Guid PostId { get; set; }
    }
}