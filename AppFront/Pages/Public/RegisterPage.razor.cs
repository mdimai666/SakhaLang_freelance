using AppFront.AuthProviders;
using AppFront.Models;
using AppShared.AuthProviders.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppFront.Pages.Public
{
    public partial class RegisterPage
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }


        protected UserForRegistrationDto _userForRegistration = new UserForRegistrationDto();
        protected bool _acceptPrivacy = false;
        


        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        private bool IsAlreadyAuth = false;

        bool regiterComplete = false;


        protected override async Task<Task> OnInitializedAsync()
        {

            var authState = await AuthState;

            IsAlreadyAuth = authState.User.Identity.IsAuthenticated;

            if(IsAlreadyAuth){
                NavigationManager.NavigateTo("/");
            }

            return base.OnInitializedAsync();
        }

        public async Task ExecuteRegister()
        {
            ShowRegistrationErrors = false;
            var result = await AuthenticationService.RegisterUser(_userForRegistration);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            else
            {
                //NavigationManager.NavigateTo("/");
                regiterComplete = true;
                StateHasChanged();
            }
        }
    }
}
