using AppFront.AuthProviders;
using AppFront.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AppFront.Pages.Public
{
    public partial class LogoutPage
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/Login");
        }
    }
}