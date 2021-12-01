using AntDesign;
using AntDesign.TableModels;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppFront.Pages.FaqPostManagementViews
{
    public partial class FaqPostManagementPage
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public List<FaqPost> Items { get; set; } = null;
        [Inject]
        public FaqPostService service { get; set; }

        ITable table;
        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;


        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task Delete(Guid id)
        {

        }

        public async Task OnChange(QueryModel<FaqPost> queryModel)
        {
            Console.WriteLine(JsonSerializer.Serialize(queryModel));


            var data = await service.ListTable(queryModel);
            if (data.Ok)
            {
                Items = data.Records.ToList();
                _total = data.TotalCount;

                Console.WriteLine(Items.Count);
            }
        }

    }
}
