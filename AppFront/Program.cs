using AntDesign;
using AppFront.AuthProviders;
using AppFront.Services;
using AppShared.Resources;
using BlastCore.Features;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;



namespace AppFront
{
    public class Program
    {


        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddAntDesign();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddHttpClientInterceptor();

            var client = new HttpClient
            {
                //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
                //BaseAddress = new Uri("http://localhost:5833")
                BaseAddress = new Uri(builder.Configuration["BackendUrl"])
            };



            builder.Services.AddScoped(sp =>
            {
                client.EnableIntercept(sp);
                return client;
            });

            builder.Services.AddScoped(sp =>
            {
                return new QServer(client);
            });

            //LocaleProvider.SetLocale("en-US");
            //var ruRU = new AntDesign.Locales.Locale();
            //LocaleProvider.SetLocale("ru-RU", ruRU);
            //builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            //builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddLocalization();
            //builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();
            builder.Services.AddScoped<IStringLocalizer<AppRes>, StringLocalizer<AppRes>>();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<OptionService>();

            builder.Services.AddScoped<CityService>();
            builder.Services.AddScoped<RoleService>();

            builder.Services.AddScoped<ChartService>();
            builder.Services.AddScoped<DocumentService>();
            builder.Services.AddScoped<PostService>();
            builder.Services.AddScoped<CommentService>();
            builder.Services.AddScoped<ContestService>();

            builder.Services.AddScoped<ContactPersonService>();
            builder.Services.AddScoped<FeedbackService>();

            builder.Services.AddScoped<ActionHistoryService>();
            builder.Services.AddScoped<ViewModelService>();
            builder.Services.AddScoped<FaqPostService>();

            builder.Services.AddScoped<GeoLocationService>();
            builder.Services.AddScoped<GeoLocationTypeService>();
            builder.Services.AddScoped<GeoMunicipalityService>();
            builder.Services.AddScoped<GeoMunicTypeService>();
            builder.Services.AddScoped<GeoRegionService>();
            builder.Services.AddScoped<GeoRegionCenterService>();
            
            builder.Services.AddScoped<LessonService>();
            builder.Services.AddScoped<PlaylistService>();
            builder.Services.AddScoped<SchoolService>();
            builder.Services.AddScoped<SchoolClassService>();
            builder.Services.AddScoped<SchoolClassPostService>();
            builder.Services.AddScoped<SchoolPostService>();
            builder.Services.AddScoped<LessonCategoryService>();

            //builder.Logging.SetMinimumLevel(LogLevel.Error);

            builder.Services.AddScoped(sp =>
            {
                Q.AddSrvProv(sp);
                return new Q();
            });



            await builder.Build().RunAsync();
        }
    }
}

/*
 * AUTH - https://docs.microsoft.com/ru-ru/aspnet/core/blazor/security/?view=aspnetcore-5.0
 * 
 */