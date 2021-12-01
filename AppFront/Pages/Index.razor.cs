using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using AntDesign;
using AppFront;
using AppFront.Shared;
using AppFront.Shared._Header;
using AppFront.Shared.ContentParts;
using AppFront.Components;
using AppFront.Services;
using AppFront.Models;
using AppShared.Models;
using AppShared.Resources;
using BlastCore.Extensions;
using System.Security.Claims;

namespace AppFront.Pages
{
    public partial class Index
    {
        [Inject] PostService postService { get; set; }
        [Inject] LessonService lessonService { get; set; }

        List<Lesson> lessons { get; set; } = new List<Lesson>();

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        MyJS myJS;

        bool Busy = true;

        //StatisticPageViewModel vm = null;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            myJS = new MyJS(JS);
            Load();
        }

        async void Load()
        {
            var result = await lessonService.ListTable(new QueryFilter(1, 10));

            if (result.Ok)
            {
                lessons = result.Records.ToList();
            }

            StateHasChanged();

            Tools.SetTimeout(() =>
            {
                _ = myJS.JqTrigger("trigger-slider1");
            }, 10);

            //vm = await viewModelService.StatisticPageViewModel();
            StateHasChanged();
        }

    }
}