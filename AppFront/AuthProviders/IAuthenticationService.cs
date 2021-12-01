using AppFront.Models;
using AppShared.AuthProviders;
using AppShared.AuthProviders.Dto;
using AppShared.Dto;
using AppShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.AuthProviders
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(AuthCreditionalsDto userForAuthentication);
        Task Logout();
    }
}
