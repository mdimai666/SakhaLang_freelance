using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.Pages.FaqPostView
{
    public partial class FaqPostPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public List<FaqPost> Items { get; set; } = null;
        [Inject]
        public FaqPostService service { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Load();
        }

        private async void Load()
        {
            var result = await service.ListTable(new QueryFilter());
            Items = result.Records.OrderBy(s => s.Title).ToList();
            StateHasChanged();
        }

        public async Task Delete(Guid id)
        {

        }

    }
}
