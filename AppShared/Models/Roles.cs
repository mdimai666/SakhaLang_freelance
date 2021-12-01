using AppShared.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    [Table("AspNetRoles")]
    public class Role : IdentityRole<Guid>, IBasicEntity, IActivatable
    {
        
        public bool Active { get; set; }
        //[Key]//public Guid Id { get; set; }//Из за этого мега баг, роли перестали работать
        public DateTime Created { get; set; }
        public DateTime Modified { get ; set; }


        //public string Name { get; set; }
        //public string NormalizedName { get; set; }
        //public string ConcurrencyStamp { get; set; }
    }
}
