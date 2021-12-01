using AntDesign;
using AntDesign.TableModels;
using AppFront.AuthProviders;
using AppFront.Models;
using AppFront.Services;
using AppShared.Models;
using BlastCore.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppFront.Pages.Settings
{
    public partial class CityListPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public List<City> Items { get; set; } = null;
        [Inject]
        public CityService service { get; set; }

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

        public async Task Delete(Guid id)
        {

        }

        public async Task OnChange(QueryModel<City> queryModel)
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


        //public EditContext editContext;
        Form<City> form;

        public readonly Guid tid = Guid.Parse("be94ca10-8cdb-4310-a448-1408a69c29a2");

        public City _model = new();

        //public void OnFinish(EditContext editContext)
        public async Task OnFinish()
        {
            City a;

            a = await service.SmartSave(_addNewCity, _model);


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
        public bool _addNewCity = false;
        [Inject]
        public MessageService _message { get; set; }

        public async void ModalSave()
        {
            Console.WriteLine("ModalSave");
            form.Submit();
        }

        public async Task OpenModalEdit(City model)
        {
            _addNewCity = false;
            _editModalVisible = true;
            _model = model;
        }

        public void AddNewItemModal()
        {
            _addNewCity = true;
            _model = new City();
            _editModalVisible = true;
        }
    }
}

