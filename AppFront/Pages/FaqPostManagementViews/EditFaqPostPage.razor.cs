using AntDesign;
using AppFront.Services;
using AppShared.Models;
using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Pages.FaqPostManagementViews
{
    public partial class EditFaqPostPage
    {
        [Parameter]
        public Guid ID { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public FaqPostService service { get; set; }
        [Inject]
        public MessageService _message { get; set; }

        [Inject]
        public HttpClient _client { get; set; }

        Form<FaqPost> form = new Form<FaqPost>();

        public FaqPost _model = new FaqPost();

        //public void OnFinish(EditContext editContext)

        bool _addNewItem = true;

        Dictionary<String, string> headers = new Dictionary<string, string>();
        string UploadAction;
        List<UploadFileItem> fileList = new List<UploadFileItem>();

        //InputFile inputFile;

        public string Accept = ".jpg,.png,.jpeg,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.pdf";

        BlazoredTextEditor QuillHtml;

        string edit_html { get; set; }


        protected override void OnInitialized()
        {
            headers.Add("Authorization", "Bearer " + Q.AuthToken);
            UploadAction = _client.BaseAddress.ToString() + "api/FaqPost/Upload";
            base.OnInitialized();

            _ = Load();
        }

        public async Task Load()
        {
            if (ID != Guid.Empty)
            {
                var app = await service.Get(ID);
                _model = app;
                edit_html = _model.Content;
                _addNewItem = false;
                StateHasChanged();
            }
        }

        public async Task OnFinish(EditContext editContext)
        {
            FaqPost a;
            _model.Content = await QuillHtml.GetHTML();

            a = await service.SmartSave(_addNewItem, _model);

            if (a is not null)
            {
                //_ = _message.Success("Сохранено");
                NavigationManager.NavigateTo("/FaqPostManagement");

            }
        }

        public void OnChangeFile(IReadOnlyList<IBrowserFile> files)
        {
            Console.WriteLine("12");
        }

        //void OnSingleCompleted(UploadInfo fileinfo)
        //{
        //    if (fileinfo.File.State == UploadState.Success)
        //    {
        //        var result = fileinfo.File.GetResponse<ResponseUploadFile>();
        //        fileinfo.File.Url = result.url;
        //        _model.FileId = result.file_id;
        //    }

        //}

        void HandleChange(UploadInfo fileinfo)
        {
            if (fileList.Count > 1)
            {
                fileList.RemoveRange(0, fileList.Count - 1);
                //fileList.Clear();
            }
            fileList.Where(file => file.State == UploadState.Success && !string.IsNullOrWhiteSpace(file.Response)).ForEach(file =>
            {
                var result = file.GetResponse<ResponseUploadFile>();
                file.Url = result.url;
            });
        }

        public async void OnDeleteClick()
        {
            var result = await service.Delete(_model.Id);

            if (result.Ok == true)
            {
                _ = _message.Success(result.Message);
                NavigationManager.NavigateTo("/FaqPostsManagement");
            }
            else
            {
                _ = _message.Error(result.Message);
            }
        }


    }
}
