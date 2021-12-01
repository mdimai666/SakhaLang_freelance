using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AntDesign;
using AppFront.Services;
using AppShared.Models;
using AntDesign.TableModels;
using AppShared.Dto;
using Newtonsoft.Json;
using AppShared.ViewModels;

namespace AppFront.Pages.UsersViews
{
    public partial class UsersPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        //[Parameter]
        public List<UserRoleDto> Items { get; set; } = null;
        [Inject]
        public UserService service { get; set; }
        [Inject]
        public MessageService messageService { get; set; }

        ITable table;
        int _pageIndex = 1;
        int _pageSize = 100;
        int _total = 0;

        string _selRoleId = null;

        List<CascaderNode> roleCascader = new List<CascaderNode>();

        UserListEditViewModel vm;

        bool Busy { get; set; } = true;

        CreateUserDto model = new();
        Form<CreateUserDto> form;

        public List<UserRoleDto> ItemsFiltered
        {
            get
            {
                string _search = filter_Search?.ToLower() ?? "";
                Guid? roleId = (string.IsNullOrEmpty(_selRoleId)) ? null : Guid.Parse(_selRoleId);


                Func<UserRoleDto, bool> predicate = s =>
                    (_search == "" || s.FullName.ToLower().Contains(_search))
                    &&
                    (roleId == null || s.Roles == null || s.Roles.Any(e => e.Id == roleId))
                ;


                return Items?.Where(predicate).ToList();
            }
        }

        //private List<CascaderNode> districts;

        //bool ZayakveKind { get; set; }
        //string filter_MunicName { get; set; } = null;

        string filter_Search { get; set; } = "";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Load();
        }

        async void Load(bool update = false)
        {
            UserListEditViewModel result = await service.UsersEditViewModel();


            if (result is null)
            {
                Busy = false;
                StateHasChanged();
                return;
            }

            vm = result;
            //Items = vm.Users;
            roleCascader = new List<CascaderNode> { new CascaderNode { Label = "Все", Value = null } };
            roleCascader.AddRange(vm.Roles.Select(s => new CascaderNode { Label = L[s.Name], Value = s.Id.ToString() }));

            if (!update)
            {
                if (vm.DefaultSelectRole == null)
                    _selRoleId = vm.Roles.First().Id.ToString();
                else
                    _selRoleId = vm.Roles.First(s => s.Id == vm.DefaultSelectRole).Id.ToString();
            }
            Busy = false;
            RefresTable();
            StateHasChanged();
        }

        void RefresTable(bool x = false)
        {
            Items = vm.Users.ToList();
            _total = Items?.Count ?? 0;
            StateHasChanged();

        }

        void SelectContestInDropDown(string val)
        {
            //Guid id = Guid.Parse(val);
            RefresTable();
        }



        //void RefresTableS(Action<List<CascaderNode<string>>> list, string x)
        //{
        //    Cascader
        //    _ = OnChange(table.GetQueryModel() as QueryModel<User>);
        //}

        public async Task DisableUser(Guid id, bool setState)
        {

        }

        public async Task AddClick()
        {
            model = new CreateUserDto();
            _visible = true;

        }



        public async Task EditClick()
        {

        }

        [Inject]
        public UserService userService { get; set; }
        public async Task OnChange(QueryModel<UserRoleDto> queryModel)
        {

            //queryModel.FilterModel.Add(new TableFilterModel2
            //{
            //    FieldName = nameof(Zayavka.IsNewProject),
            //    SelectedValues = new string[] { ZayakveKind.ToString() },
            //}
            //);

            //queryModel.FilterModel.Add(new TableFilterModel2
            //{
            //    FieldName = nameof(Zayavka.MunicName),
            //    SelectedValues = new string[] { filter_MunicName },
            //}
            //);

            //Console.WriteLine(JsonSerializer.Serialize(queryModel));

            //var data = await service.ListTable(queryModel.AsQueryFilter(filter_Search));

            //if (data.Ok)
            //{
            //    //Items = data.Records.ToList();
            //    Items = new List<Zayavka>();
            //    //_total = data.TotalCount;

            //    //Console.WriteLine(Items.Count);

            //    StateHasChanged();
            //}
        }

        //----------
        bool _visible = false;

        bool isNewProject = true;
        bool _modeCreareButtonLoading = false;


        private async void HandleOk(MouseEventArgs e)
        {
            //Console.WriteLine(e);
            //_visible = false;

            if (!form.Validate())
            {
                return;
            }
            _modeCreareButtonLoading = true;


            Console.WriteLine(JsonConvert.SerializeObject(model));

            //NavigationManager.NavigateTo("/applicatione/edit");

            var result = await service.CreateUserDto(model);

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
                model = new CreateUserDto();
                Load(true);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }

            //var addnew = await service.Add(new User
            //{
            //    Title = "",
            //    Description = "��������",
            //    IsNewProject = isNewProject,

            //});

            _modeCreareButtonLoading = false;
            StateHasChanged();

            //Guid id = addnew.Id;
            //NavigationManager.NavigateTo($"/applicatione/edit/{id}");
        }

        private void HandleCancel(MouseEventArgs e)
        {
            Console.WriteLine(e);
            _visible = false;
        }



    }
}
