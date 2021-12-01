using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppShared.Dto;
using AppShared.Models;
using AppShared.ViewModels;
using Blazored.LocalStorage;

namespace AppFront.Services
{
    public class UserService : BasicClientService<User>
    {
        private readonly ILocalStorageService _localStorage;

        public UserService(IServiceProvider serviceProvider, HttpClient httpClient) : base(serviceProvider, httpClient)
        {
            this._localStorage = serviceProvider.GetService(typeof(ILocalStorageService)) as ILocalStorageService;
        }

        public async Task<User> GetProfile()
        {
            //if(Q.Profile.)
            Console.WriteLine(">>profile:" + Q.Profile.Id);
            if (string.IsNullOrEmpty(Q.Profile.Id))
            {
                await Task.Delay(1000);
            }

            return await Get(Guid.Parse(Q.Profile.Id));
        }

        public string GetToken()
        {
            var token = _localStorage.GetItemAsync<string>("authToken").Result;

            return token;
        }

        public async Task<UserListEditViewModel> UsersEditViewModel()
        {
            UserListEditViewModel result =
                await _client.GET<UserListEditViewModel>($"{_basePath}{_controllerName}/UsersEditViewModel");
            return result;
        }

        public async Task<UserActionResult<CreateUserDto>> CreateUserDto(CreateUserDto dto)
        {
            var result = await _client.Post<UserActionResult<CreateUserDto>>($"{_basePath}{_controllerName}/CreateUser", dto);
            return result;
        }


        public async Task<UserProfileInfoDto> UserProfileInfo(Guid id)
        {
            UserProfileInfoDto result =
                await _client.GET<UserProfileInfoDto>($"{_basePath}{_controllerName}/UserProfileInfo/{id}");
            return result;
        }

        public async Task<UserEditProfileDto> UserEditProfile(Guid id)
        {
            UserEditProfileDto result =
                await _client.GET<UserEditProfileDto>($"{_basePath}{_controllerName}/UserEditProfile/{id}");
            return result;
        }

        public async Task<UserActionResult<UserEditProfileDto>> UserEditProfileUpdate(UserEditProfileDto dto)
        {
            UserActionResult<UserEditProfileDto> result =
                await _client.PUT<UserActionResult<UserEditProfileDto>>($"{_basePath}{_controllerName}/UserEditProfile/{dto.Id}", dto);
            return result;
        }

        public async Task<UserActionResult> UpdateUserRoles(Guid id, IEnumerable<Guid> roles)
        {
            UserActionResult result = await _client.PUT<UserActionResult>($"{_basePath}{_controllerName}/UpdateUserRoles/{id}", roles);
            return result;
        }

        public async Task<UserActionResult> SetUserState(Guid id, bool state)
        {
            UserActionResult result = await _client.PUT<UserActionResult>($"{_basePath}{_controllerName}/SetUserState/{id}", new[] { state });
            return result;
        }
    }

}