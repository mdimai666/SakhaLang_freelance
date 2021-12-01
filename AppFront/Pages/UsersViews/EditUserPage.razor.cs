using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using AntDesign;
using AppFront.Services;
using AppShared.Dto;
using AppShared.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace AppFront.Pages.UsersViews
{
    public partial class EditUserPage
    {
        [Parameter]
        public Guid ID { get; set; }

        public UserEditProfileDto user { get; set; }

        [Inject] public MessageService messageService { get; set; }
        [Inject] public UserService userService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] public ViewModelService viewModelService { get; set; }

        EditUserViewModel vm;

        public IEnumerable<Guid> UpdUserRoles { get => vm.User.Roles.Select(s => s.Id); set => vm.User.Roles = vm.Roles.Where(s => value.Contains(s.Id)); }

        protected override Task OnInitializedAsync()
        {
            if (ID == Guid.Empty)
                ID = Q.User.Id;
            return base.OnInitializedAsync();
        }

        async Task<UserEditProfileDto> Get()
        {
            //return await userService.UserEditProfile(ID == Guid.Empty ? Q.User.Id : ID);
            vm = await viewModelService.EditUserViewModel(ID == Guid.Empty ? Q.User.Id : ID);

            return new UserEditProfileDto(vm.User);
        }

        async void OnFinish(EditContext context)
        {
            var result = await userService.UserEditProfileUpdate(user);

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
                user = result.Data;

            }
            else
            {
                _ = messageService.Error(result.Message);
            }
            StateHasChanged();

        }

        async void OnDeleteClick()
        {
            var result = await userService.Delete(user.Id);

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
                navigationManager.NavigateTo("/Users");
            }
            else
            {
                _ = messageService.Error(result.Message);
            }
        }

        async void DisableUser(bool setState)
        {
            vm.User.LockoutEnabled = setState;
            //Console.WriteLine($"LockoutEnabled={vm.User.LockoutEnabled}");

            var result = await userService.SetUserState(user.Id, setState);

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }

            StateHasChanged();
        }

        async void UpdateUserRoles()
        {
            var result = await userService.UpdateUserRoles(user.Id, UpdUserRoles);

            if (result.Ok)
            {
                _ = messageService.Success(result.Message);
            }
            else
            {
                _ = messageService.Error(result.Message);
            }
        }
    }
}