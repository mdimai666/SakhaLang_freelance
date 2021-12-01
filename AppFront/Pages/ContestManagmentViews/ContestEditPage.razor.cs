using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using AntDesign;
using AppFront.Services;
using AppFront.Models;
using AppShared.Models;
using System.Text.Json;
using AntDesign.TableModels;

namespace AppFront.Pages.ContestManagmentViews
{
    public partial class ContestEditPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<Contest> Items { get; set; } = null;
        [Inject]
        public ContestService service { get; set; }
        [Inject]
        public MessageService _message { get; set; }


        Contest _model = new Contest();
        Form<Contest> form = new Form<Contest>();

        ITable table;
        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;

        bool _addNew;

        bool _visible = false;

        bool isNewProject = true;
        bool _modeCreareButtonLoading = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Load();
        }

        async void Load()
        {

        }



        void OnChange2()
        {
            _ = OnChange(table.GetQueryModel() as QueryModel<Contest>);
        }


        public async Task OnChange(QueryModel<Contest> queryModel)
        {

            Console.WriteLine(JsonSerializer.Serialize(queryModel));

            var data = await service.ListTable(queryModel.AsQueryFilter());

            if (data.Ok)
            {
                Items = data.Records.ToList();
                _total = data.TotalCount;

                Console.WriteLine(Items.Count);

                StateHasChanged();
            }
        }

        public async Task Delete(Guid id)
        {
            var result = await service.Delete(id);
            if (result.Ok)
            {
                _ = _message.Success(result.Message);
            }
            else
            {
                _ = _message.Error(result.Message);
            }
            OnChange2();
        }

        public async Task AddClick()
        {
            _addNew = true;
            _model = new Contest();
            _visible = true;
        }

        public void EditClick(Contest contest)
        {
            _addNew = false;
            _model = contest;
            _visible = true;
        }



        private async void HandleOk(EditContext editContext)
        {
            Contest a;

            _modeCreareButtonLoading = true;

            a = await service.SmartSave(_addNew, _model);

            if (a is not null)
            {
                _visible = false;
            }

            _modeCreareButtonLoading = false;
            OnChange2();
            StateHasChanged();

        }

        private void HandleCancel()
        {
            //Console.WriteLine(e);
            _visible = false;
            _addNew = false;
        }

    }
}