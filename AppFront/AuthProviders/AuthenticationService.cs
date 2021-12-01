using AppFront.Models;
using AppShared.AuthProviders;
using AppShared.AuthProviders.Dto;
using AppShared.Dto;
using AppShared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace AppFront.AuthProviders
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage, HttpClient httpClient)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _httpClient = httpClient;
        }


        public async Task<AuthResponseDto> Login(AuthCreditionalsDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var authResult = await _client.PostAsync("/api/Account/Login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!authResult.IsSuccessStatusCode)
                return result;

            await LoginStage(result);

            return new AuthResponseDto { IsAuthSuccessful = true };
        }

        async Task LoginStage(AuthResponseDto result)
        {
            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            //var profile = await GetProfile();
            //Q.UpdateProfile(profile);
            await TryUpdateProfile();
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            // https://code-maze.com/blazor-webassembly-registration-aspnetcore-identity/
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync("/api/Account/RegisterUser", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
            //if (!registrationResult.IsSuccessStatusCode)
            //{
            //    var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //    return result;
            //}
            //return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<Profile> GetProfile()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Profile>("/api/Account/GetProfile");
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public async Task TryUpdateProfile()
        {
            // Console.WriteLine("TryUpdateProfile");
            var profile = await GetProfile();
            Q.UpdateProfile(profile);
        }

        public async Task<UserActionResult> ThirdLogin(string serviceName, string token)
        {
            //string returnurl = "";

            try
            {
                var content = JsonSerializer.Serialize(new[] { token });
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                //var bodyContent = new StringContent(content, Encoding.UTF8, "text/plain");
                var authResult = await _client.PostAsync("/api/Account/EsiaLogin", bodyContent);
                var authContent = await authResult.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result.IsAuthSuccessful)
                {
                    await LoginStage(result);
                }

                return new UserActionResult
                {
                    Ok = result.IsAuthSuccessful,
                    Message = result.ErrorMessage
                };
                
            }
            catch (Exception ex)
            {
                return new UserActionResult
                {
                    Ok = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
