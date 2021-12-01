using AntDesign;
using AppFront.AuthProviders;
using AppFront.Models;
using AppShared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;
using System.Web;

namespace AppFront.Pages.Public
{
    public partial class LoginPage
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        private AuthCreditionalsDto auth = new();

        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public MessageService messageService { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        private bool IsAlreadyAuth = false;

        public string AuthProvider { get; set; } = "";
        //[SupplyParameterFromQuery] 
        public string ReturnUrl { get; set; }
        //[SupplyParameterFromQuery] 
        public string Data { get; set; }

        bool IsLoginLoading = false;

        protected override void OnInitialized()
        {
#if DEBUG

            auth.Login = "mdimai666@mail.ru";
            auth.Password = "Admin123!";

#endif
            Load();
        }


        async void Load()
        {
            base.OnInitialized();

            var authState = await AuthState;

            IsAlreadyAuth = authState.User.Identity.IsAuthenticated;

            if (IsAlreadyAuth)
            {
                NavigationManager.NavigateTo("/");
            }



            var querystring = HttpUtility.ParseQueryString(new Uri(NavigationManager.Uri).Query);

            Data = querystring["data"];
            ReturnUrl = querystring["returnurl"];

            Console.WriteLine($"Data={Data}");
            Console.WriteLine($"ReturnUrl={ReturnUrl}");

            if (!string.IsNullOrEmpty(Data) && !string.IsNullOrEmpty(ReturnUrl))
            {
                IsLoginLoading = true;
                AuthProvider = "esia";
                StateHasChanged();
                SendThirdAuthDataToServer();
            }
        }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            var result = await AuthenticationService.Login(auth);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }

        public async void ThirdLogin(string name = "esia")
        {
#if DEBUG
            NavigationManager.NavigateTo("/Login/?data=123&returnurl=/", true);
#else
            NavigationManager.NavigateTo("https://yakutia.click/accounts/esia/login/?returnurl=https://do.sakha.education/");
#endif

        }

        async void SendThirdAuthDataToServer()
        {
            AuthenticationService authenticationService = AuthenticationService as AuthenticationService;

            var result = await authenticationService.ThirdLogin(AuthProvider, Data);
            IsLoginLoading = false;

            if (result.Ok)
            {
                _ = messageService.Success("success");
                NavigationManager.NavigateTo(ReturnUrl ?? "/");

            }
            else
            {
                ShowAuthError = true;
                Error = result.Message;
                //_ = messageService.Error(result.Message);
            }
            StateHasChanged();
        }
    }
}

/*

Use in code

@code {
    private int currentCount = 0;
    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; }
    private async void IncrementCount()
    {
        var authState = await AuthState;
        var user = authState.User;
        if (user.Identity.IsAuthenticated)
            currentCount++;
        else
            currentCount--;
    }
}

*/