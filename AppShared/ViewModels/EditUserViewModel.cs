using AppShared.Dto;
using AppShared.Models;
using AppShared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.ViewModels
{
    public class EditUserViewModel 
    {
        public UserRoleDto User { get; set; }
        public IEnumerable<Role> Roles { get; set; }

    }

    public interface IViewModelBasic
    {
        //public IViewModelBasic Create(IServiceProvider serviceProvider, ApplicationDbContext ef);
    }
}
