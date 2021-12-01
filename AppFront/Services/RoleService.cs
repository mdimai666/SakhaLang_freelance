using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppShared.Dto;
using AppShared.Models;


namespace AppFront.Services
{
    public class RoleService : BasicClientService<Role>
    {
        public RoleService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {


        }

        public async Task<EditRolesViewModelDto> EditRolesViewModel()
        {
            EditRolesViewModelDto result =
                await _client.GET<EditRolesViewModelDto>($"{_basePath}{_controllerName}/EditRolesViewModel");
            return result;
        }
    }
}
