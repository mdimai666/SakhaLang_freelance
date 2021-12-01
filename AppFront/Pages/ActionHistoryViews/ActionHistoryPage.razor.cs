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

namespace AppFront.Pages.ActionHistoryViews
{
    public partial class ActionHistoryPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<ActionHistory> Items { get; set; } = null;
        [Inject]
        public ActionHistoryService service { get; set; }
        [Inject]
        public MessageService _message { get; set; }


        ActionHistory _model = new ActionHistory();
        Form<ActionHistory> form = new Form<ActionHistory>();

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
            _ = OnChange(table.GetQueryModel() as QueryModel<ActionHistory>);
        }


        public async Task OnChange(QueryModel<ActionHistory> queryModel)
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
            _model = new ActionHistory();
            _visible = true;
        }

        public void EditClick(ActionHistory contest)
        {
            _addNew = false;
            _model = contest;
            _visible = true;
        }



        private async void HandleOk(EditContext editContext)
        {
            ActionHistory a;

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