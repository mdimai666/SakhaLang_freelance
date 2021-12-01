using AntDesign;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;

namespace AppFront.Components
{
    public partial class StandartEditForm1<TModel, TService>
        where TModel : BasicEntityNonUser, new()
        where TService : BasicClientService<TModel>
    {


        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public TService service { get; set; }
        [Inject] public MessageService _message { get; set; }


        [Parameter] public RenderFragment<TModel> ChildContent { get; set; } = null;



        public TModel _model = new();
        public Form<TModel> form = new Form<TModel>();

        public bool _addNew;
        public bool _visible = false;
        public bool _modeCreareButtonLoading = false;

        //        //Guid _value;
        //        [Parameter]
        //        public TModel Value
        //        {
        //            get => _model;
        //            set
        //            {
        //                if (value == _model) return;
        //                //if (value == Guid.Empty) return;
        //#if DEBUG
        //                Console.WriteLine($"C:StandartEditForm1 Model {1} -> {value} ");
        //#endif
        //                //bool cc = _value == Guid.Empty;
        //                _model = value;
        //                ValueChanged.InvokeAsync(_model).Wait();
        //                //if(!cc)
        //                {
        //                    //var f = VarianList.FirstOrDefault(s => s.Id == _value);
        //                    //OnValueChange.InvokeAsync(f).Wait();
        //                }
        //            }
        //        }
        //        [Parameter] public EventCallback<TModel> ValueChanged { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
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


        public async void HandleOk()
        {
            TModel a;

            _modeCreareButtonLoading = true;

            a = await service.SmartSave(_addNew, _model);


            if (a is not null)
            {
                _visible = false;
            }

            _modeCreareButtonLoading = false;
            //OnChange2();
            StateHasChanged();

        }

        public void HandleCancel()
        {
            //Console.WriteLine(e);
            _visible = false;
            _addNew = false;
        }
    }
}