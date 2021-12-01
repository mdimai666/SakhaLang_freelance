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
using BlastCore.Extensions;

namespace AppFront.Pages.NewsView
{
    public partial class NewsList1
    {
        [Parameter]
        public List<Post> Items { get; set; } = null;
        [Inject]
        public PostService service { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Load2();
        }


        async void Load2()
        {
            var result = await service.ListTable(new QueryFilter(1, 10));
            Items = result.Records.ToList();
            StateHasChanged();
        }
    }
}