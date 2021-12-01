using Microsoft.AspNetCore.Components;
using AppFront.Services;
using System.Collections.Generic;
using AppShared.Models;
using System.Linq;
using AppShared.ViewModels;
using BlastCore.Extensions;
using Microsoft.JSInterop;

namespace AppFront.Pages.StatisticsView
{
    public partial class StatisticsPage
    {
        [Inject] PostService postService { get; set; }

        List<Post> news { get; set; } = new List<Post>();

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        MyJS myJS;

        StatisticPageViewModel vm = null;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            myJS = new MyJS(JS);
            Load();
        }

        async void Load()
        {
            news = await postService.GetNews();
            StateHasChanged();

            Tools.SetTimeout(() =>
            {
                _ = myJS.JqTrigger("trigger-slider1");
            }, 10);

            vm = await viewModelService.StatisticPageViewModel();
            StateHasChanged();
        }

    }
}