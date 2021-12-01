using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.ViewModels
{
    public interface IViewModelService
    {
        Task<EditUserViewModel> EditUserViewModel(Guid id);
    }
}
