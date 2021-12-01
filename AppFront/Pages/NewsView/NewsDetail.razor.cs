using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.Pages.NewsView
{
    public partial class NewsDetail
    {

        [Parameter]
        public Guid ID {  get; set; }

        public Post Post { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        PostService postService { get; set; }


        protected override async void OnInitialized()
        {
            base.OnInitialized();

            //HttpContext.Current.Request.Url.Authority


            Post = await postService.Get(ID);
            StateHasChanged();

        }
    }
}
