using AntDesign;
using AntDesign.TableModels;
using AppFront.Models;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Text.Json;

namespace AppFront.Components
{
    public partial class StandartManagementTable<TModel, TService> : AntDomComponentBase
        where TModel : IBasicEntity, new()
        where TService : BasicClientService<TModel>
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        List<TModel> Items { get; set; } = null;
        [Inject] public TService service { get; set; }
        [Inject] public MessageService _message { get; set; }
        [Inject] public ConfirmService _confirmService { get; set; }

        [Parameter] public RenderFragment<TModel> TableContent { get; set; } = null;
        [Parameter] public RenderFragment<TModel> FormContent { get; set; } = null;

        [Parameter] public Expression<Func<TModel, bool>> SearchExpression { get; set; } = null;


        TModel _model = new();
        Form<TModel> form = new Form<TModel>();

        ITable table;
        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;

        bool _addNew;

        bool _visible = false;

        bool _modeCreareButtonLoading = false;

        public string SearchText = "";



        protected override void OnInitialized()
        {
#if DEBUG
            //Console.WriteLine("StandartManagementTable:INIT");
#endif
            base.OnInitialized();
            Load();
        }

        async void Load()
        {

        }


        void OnChange2()
        {
            _ = OnChange(table.GetQueryModel() as QueryModel<TModel>);
        }


        public async Task OnChange(QueryModel<TModel> queryModel)
        {

            Console.WriteLine(JsonSerializer.Serialize(queryModel));

            QueryFilter queryFilter = queryModel.AsQueryFilter();

            if (SearchExpression is not null && !string.IsNullOrEmpty(SearchText))
            {
                queryFilter.AddQuery<TModel>(SearchExpression);
                Console.WriteLine("XXX");
            }

            var data = await service.ListTable(queryFilter);

            if (data.Ok)
            {
                Items = data.Records.ToList();
                _total = data.TotalCount;
                //Console.WriteLine(Items.Count);
                StateHasChanged();
            }
        }

        void OnSearchChange(string text)
        {
            OnChange2();
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
            _visible = false;
            OnChange2();
        }

        public async void OnModelDeleteClick(Guid id)
        {
            var result = await _confirmService.Show("", "Уверены что хотите удалить?", ConfirmButtons.OKCancel);

            if (result == ConfirmResult.OK) _ = Delete(id);
        }

        public async Task AddClick()
        {
            _addNew = true;
            _model = new TModel();
            _visible = true;
        }

        public void EditClick(TModel contest)
        {
            _addNew = false;
            _model = contest;
            _visible = true;
        }


        private async void HandleOk(EditContext editContext)
        {
            TModel a;

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