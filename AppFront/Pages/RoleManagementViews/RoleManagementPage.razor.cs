using AntDesign;
using AntDesign.TableModels;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppFront.Pages.RoleManagementViews
{
    public partial class RoleManagementPage
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public MessageService messageService { get; set; }
        [Parameter]
        public List<Role> Items { get; set; } = null;
        [Inject]
        public RoleService service { get; set; }

        ITable table;
        int _pageIndex = 1;
        int _pageSize = 10;
        int _total = 0;

        protected override async Task<Task> OnInitializedAsync()
        {
            //_model = new City { Name = "German", Currency = "G", CurrencySymbol = "S", Location = "162.0" };

            //editContext = new EditContext(_model);

            return base.OnInitializedAsync();
        }


        public async Task OnChange(QueryModel<Role> queryModel)
        {
            Console.WriteLine(JsonSerializer.Serialize(queryModel));


            TotalResponse<Role> data = await service.ListTable(queryModel);

            var result = data.Result;


            if (data.Ok)
            {
                Items = data.Records.ToList();
                _total = data.TotalCount;

                Console.WriteLine(Items.Count);
            }
        }

        Form<Role> form;

        public readonly Guid tid = Guid.Parse("be94ca10-8cdb-4310-a448-1408a69c29a2");

        public Role _model = new();

        public async Task OnFinish()
        {
            Role a;

            a = await service.SmartSave(_addNewRole, _model);

            if (a is not null)
            {
                //_ = _message.Success("Сохранено");
                _editModalVisible = false;

            }
        }

        public void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }


        public bool _editModalVisible = false;
        public bool _addNewRole = false;
        [Inject]
        public MessageService _message { get; set; }

        public async void ModalSave()
        {
            Console.WriteLine("ModalSave");
            form.Submit();
        }

        public async Task OpenModalEdit(Role model)
        {
            _addNewRole = false;
            _editModalVisible = true;
            _model = model;
        }

        public void AddNewItemModal()
        {
            _addNewRole = true;
            _model = new Role();
            _editModalVisible = true;
        }

        async void SaveClick()
        {
            await Task.Delay(1000);
            _ = messageService.Error("Не удалось сохранить");
        }
    }
}
