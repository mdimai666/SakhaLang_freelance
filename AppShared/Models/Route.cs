using AppShared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShared.Models
{
    [Table("Routes")]
    public class Route : BasicEntity
    {
        public string FromLat { get; set; }
        public string FromLon { get; set; }
        public string FromAddress { get; set; }
        public string ToLat { get; set; }
        public string ToLon { get; set; }
        public string ToAddress { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public float DistanceKm { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public decimal Price { get; set; }
        public City City { get; set; }
        [ForeignKey(nameof(AppShared.Models.City))]
        public Guid CityId { get; set; }

    }

    
}