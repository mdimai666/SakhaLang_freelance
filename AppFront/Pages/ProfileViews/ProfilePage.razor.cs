using AntDesign;
using AppFront.Services;
using AppShared.Dto;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Pages.ProfileViews
{
    public partial class ProfilePage
    {

        [Inject]
        public UserService userService { get; set; }
        [Inject]
        IJSRuntime JS { get; set; }
        [Inject]
        public HttpClient _client { get; set; }
        [Inject]
        public MessageService _message { get; set; }

        Form<User> form = new Form<User>();

        public User _model = new User();

        Dictionary<String, string> headers = new Dictionary<string, string>();

        
        private MyJS myJS;
        public UserProfileInfoDto user { get; set; }
        string UploadAction;

        public string Accept = ".jpg,.png,.jpeg";

        protected override void OnInitialized()
        {
            myJS = new(JS);
            UploadAction = _client.BaseAddress.ToString() + "api/Account/UploadAvatar";

            headers.Add("Authorization", "Bearer " + Q.AuthToken);

            base.OnInitialized();
            //Load();
        }

        //async void Load()
        //{
        //    while ((String.IsNullOrEmpty(Q.Profile.Id)) || Q.Profile.Id == "0")
        //    {
        //        await Task.Delay(200);
        //    }
        //    Console.WriteLine(">>p:" + Q.Profile.Id);
        //    var result = await userService.Get(Guid.Parse(Q.Profile.Id));
        //    if(result != null)
        //    {
        //        user = result;
        //        StateHasChanged();
        //    }
        //}

        void OnSingleCompleted(UploadInfo fileinfo)
        {
            //Console.WriteLine(fileinfo.File?.FileName);
            if (fileinfo.File.State == UploadState.Success)
            {
                //Console.WriteLine(JsonConvert.SerializeObject(fileinfo.File));
                //_message.Success(fileinfo.File?.Url);
                var result = fileinfo.File.GetResponse<ResponseUploadFile>();
                fileinfo.File.Url = result.url;
                _message.Success(result.url);
            }
            StateHasChanged();

        }

        public async Task OnFinish(EditContext editContext)
        {
            //Document a;

            //if (_addNewItem)
            //{
            //    a = await service.Add(_model, file1);
            //}
            //else
            //{
            //    a = await service.Update(_model);
            //}


            //if (a is not null)
            //{

            //}
            _ = _message.Success("Сохранено");
        }

    }
}
