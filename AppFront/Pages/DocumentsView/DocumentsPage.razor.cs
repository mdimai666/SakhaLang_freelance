using AntDesign.TableModels;
using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFront.Services;
using System.Text.Json;
using AppShared.Models;

namespace AppFront.Pages.DocumentsView
{
    public partial class DocumentsPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public List<Document> Items { get; set; } = null;
        [Inject]
        public DocumentService service { get; set; }

        //ITable table;
        //int _pageIndex = 1;
        //int _pageSize = 10;
        //int _total = 0;
        //IEnumerable<Document> selectedRows;


        protected override void OnInitialized()
        {
            base.OnInitialized();
            Load();
        }

        private async void Load()
        {
            var result = await service.ListTable(new QueryFilter());
            Items = result.Records.ToList();
            StateHasChanged();
        }

        public async Task Delete(Guid id)
        {

        }

        //public async Task OnChange(QueryModel<Document> queryModel)
        //{
        //    Console.WriteLine(JsonSerializer.Serialize(queryModel));


        //    var data = await service.ListTable(queryModel);
        //    if (data.Ok)
        //    {
        //        Items = data.Records.ToList();
        //        _total = data.TotalCount;

        //        Console.WriteLine(Items.Count);
        //    }
        //}
    }
}
