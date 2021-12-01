using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.Pages.NewsView
{
    public partial class NewsPage
    {
        public List<Post> Posts { get; set; }

        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] PostService postService { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        MyJS myJS;

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            //HttpContext.Current.Request.Url.Authority
            myJS = new MyJS(JS);

            Posts = await postService.GetNews();
            //await myJS.JqTrigger("trigger-slider1");
            StateHasChanged();
        }
    }
}
