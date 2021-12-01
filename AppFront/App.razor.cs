

using AppFront.AuthProviders;
using AppFront.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace AppFront
{
    public partial class App
    {

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }
        [Inject] protected AuthenticationService AuthenticationService { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }


        [Inject] HttpClientInterceptor Interceptor { get; set; }
        [Inject] ViewModelService viewModelService { get; set; }

        [Inject] ILogger<App> _logger { get; set; }

        bool isLoading = true;
        bool isSuccess = false;

        protected override Task OnInitializedAsync()
        {
            //Initialize().GetAwaiter().GetResult();
            Initialize();

            return base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                // System.Console.WriteLine("OnAfterRender::First");
                //Initialize();
            }

            // JSRuntime.InvokeVoidAsync("d_onPageLoad");

            //Console.WriteLine("RRendred!");
        }

        private void Initialize()
        {

            Console.WriteLine("App.Initialize");

            Interceptor.AfterSend += Interceptor_AfterSend!;

            Q.Root.On("FirstGetAuthenticationStateAsync", FirstGetAuthenticationStateAsync);

            Load();
        }

        async void Load()
        {
            try
            {
                isSuccess = await viewModelService.TryUpdateInitialSiteData();
                isLoading = false;
            }
            catch (Exception)
            {
                isLoading = false;
                //throw;
            }
            StateHasChanged();
        }

        private void Interceptor_AfterSend(object sender, HttpClientInterceptorEventArgs e)
        {
            //throw new NotImplementedException();
            //Console.WriteLine("ee");
            if (e.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogWarning("App::Unauthorized");
                Task.Run(async () =>
                {
                    await AuthenticationService.Logout();
                    NavigationManager.NavigateTo("/Login");
                });
            }
        }

        void FirstGetAuthenticationStateAsync()
        {
            _ = AuthenticationService.TryUpdateProfile();
        }
    }
}