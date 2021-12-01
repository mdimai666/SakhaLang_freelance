using AntDesign;
using AppFront.Features;
using BlastCore.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppFront.AuthProviders
{
    public class AuthStateProvider : AuthenticationStateProvider
    {

        bool _firstStateCheck = false;

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Console.WriteLine("GetAuthenticationStateAsync");
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                return _anonymous;

            Q.AuthToken = token;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (_firstStateCheck == false)
            {
                Q.Root.Emit("FirstGetAuthenticationStateAsync");
            }
            _firstStateCheck = true;

            var ci = new ClaimsPrincipal(new ClaimsIdentity(
                JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"
            ));
            AuthenticationState state = new AuthenticationState(ci);

            //List<string> e = new List<string>();

            //ci.Claims.ForEach(s => e.Add($"{s.Type}:{s.Value}"));

            //Console.WriteLine("Sss>" + e.JoinStr(";\n ").ToString());

            //var u = new UserFromClaims(ci);
            //Console.WriteLine("ee>>" + JsonConvert.SerializeObject(u));
            Q.UpdateClaimUser(ci);

            //Console.WriteLine("state:");
            //Console.WriteLine(state);

            return state;
        }

        public void NotifyUserAuthentication(string token)
        {
            var ci = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authenticatedUser = ci;
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            Q.AuthToken = token;
            Q.UpdateClaimUser(ci);

            //Console.WriteLine("ee>>" + JsonConvert.SerializeObject(ci));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }



    }
}
