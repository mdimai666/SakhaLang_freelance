using AppShared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("Supports")]
    public class Support : BasicEntity
    {
        public City City { get; set; }
        [ForeignKey(nameof(AppShared.Models.City))]
        public Guid CityId { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
    }

    
}