using AppShared.Dto;
using AppShared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.ViewModels
{
    public class UserListEditViewModel
    {
        public List<UserRoleDto> Users { get; set; }
        
        [Display(Name = "Роли")]
        public IEnumerable<Role> Roles { get; set; }

        public Guid? DefaultSelectRole { get; set; }


    }

}
