using AntDesign;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;

namespace AppFront.Components
{
    public partial class DSelectGuidServ<TModel, TService>
        where TModel : IBasicEntity, new()
        where TService : BasicClientService<TModel>
    {

        Guid _value;
        [Parameter]
        public Guid Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                //if (value == Guid.Empty) return;
#if DEBUG
                Console.WriteLine($"C:TModel Model {_value} -> {value} ");
#endif
                //bool cc = _value == Guid.Empty;
                _value = value;
                ValueChanged.InvokeAsync(_value).Wait();
                //if(!cc)
                {
                    //var f = VarianList.FirstOrDefault(s => s.Id == _value);
                    //OnValueChange.InvokeAsync(f).Wait();
                }
            }
        }
        [Parameter] public EventCallback<Guid> ValueChanged { get; set; }
        [Parameter] public EventCallback<TModel?> OnValueChange { get; set; }


        [Inject] TService service { get; set; }

        [Parameter] public Func<TModel, string> LabelExpression { get; set; }

        IEnumerable<TModel> VarianList = new List<TModel>();

        string errorMessage = null;

        IEnumerable<CascaderNode> SelectNodes = new List<CascaderNode>();

        bool Busy = true;

        public string SettetStringId
        {
            get => _value.ToString();
            set
            {
                Guid id = Guid.Parse(value);
                //if (id == _value) return;
                //Console.WriteLine($"C:SettetStringId {_value} -> {value}");
                Value = id;
                //var f = VarianList.FirstOrDefault(x => x.Id == id);
                //if (f != null)
                //{
                //    //ValueId = id;
                //}
                //StateHasChanged();
            }
        }

        protected override void OnInitialized()
        {
#if DEBUG
            //Console.WriteLine("DSelectGuidServ:INIT");
#endif
            base.OnInitialized();
            Load();
        }

#if DEBUG
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                //Console.WriteLine("DSelectGuidServ:OnAfterRender-firstRender");
            }
            else
            {
                //Console.WriteLine("DSelectGuidServ:OnAfterRender");
            }

            base.OnAfterRender(firstRender);
        }
#endif

        async void Load()
        {
            Busy = true;
            var result = await service.ListTable(new QueryFilter(1, 30));
            Busy = false;
            if (result.Ok)
            {
                VarianList = result.Records;
                SelectNodes = new List<CascaderNode>{
                    new CascaderNode {
                        Value = Guid.Empty.ToString(),
                        Label = "-не выбрано-"
                    }
                };
                SelectNodes = SelectNodes.Concat(

                    VarianList.Select(s =>
                    new CascaderNode
                    {
                        Value = s.Id.ToString(),
                        Label = (LabelExpression is not null) ? LabelExpression(s) : (s.Id.ToString())
                    }
                ));
            }
            else
            {
                errorMessage = result.Message ?? "error";
            }
            StateHasChanged();
        }

        //protected override bool ShouldRender()
        //{
        //    //return base.ShouldRender();
        //    return false;
        //}
    }
}