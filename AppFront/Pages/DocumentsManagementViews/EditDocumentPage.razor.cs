using AntDesign;
using AppFront.Services;
using AppShared.Models;
using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppFront.Pages.DocumentsManagementViews
{
    public partial class EditDocumentPage
    {
        [Parameter]
        public Guid ID { get; set; }

        private MyJS myJS;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public DocumentService service { get; set; }
        [Inject]
        public MessageService _message { get; set; }

        [Inject]
        public HttpClient _client { get; set; }

        Form<Document> form = new Form<Document>();

        public Document _model = new Document();

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
            myJS = new(JS);
            headers.Add("Authorization", "Bearer " + Q.AuthToken);
            UploadAction = _client.BaseAddress.ToString() + "api/Document/Upload";
            _model = new Document()
            {
                //File = new FileEntity()
            };
            form = new Form<Document>();
            base.OnInitialized();

            _ = Load();
        }

        public async Task Load()
        {
            if (ID != Guid.Empty)
            {
                var app = await service.Get(ID);
                var f = app.File.FileName;
                var ext = string.IsNullOrEmpty(app.File.FileExt) ? "txt" : app.File.FileExt;
                Console.WriteLine($"f={f}");
                fileList.Add(new UploadFileItem
                {
                    Id = app.File.Id.ToString(),
                    FileName = f.Contains(ext) ? f : $"{f}.{ext}",
                    //Percent = 100,//2
                    //Ext = "." + app.File.FileExt,//2
                    //Type = "image/" + app.File.FileExt,//2
                    //Size = (long)app.File.FileSize,//2
                    State = UploadState.Success,
                    Url = app.File.FileUrl,
                    //ObjectURL = app.File.FileUrl,//2
                    //Response = JsonConvert.SerializeObject(new ResponseUploadFile(app.File))//2,
                });
                _model = app;
                edit_html = _model.Description;
                _addNewItem = false;
                StateHasChanged();
            }
        }

        public async Task OnFinish(EditContext editContext)
        {
            Document a;
            _model.Description = await QuillHtml.GetHTML();

            a = await service.SmartSave(_addNewItem, _model);

            if (a is not null)
            {
                NavigationManager.NavigateTo("/DocumentsManagement");
                //_ = _message.Success("Сохранено");

            }
        }

        public void OnChangeFile(IReadOnlyList<IBrowserFile> files)
        {
            Console.WriteLine("12");
        }

        void OnSingleCompleted(UploadInfo fileinfo)
        {
            if (fileinfo.File.State == UploadState.Success)
            {
                var result = fileinfo.File.GetResponse<ResponseUploadFile>();
                fileinfo.File.Url = result.url;
                _model.FileId = result.file_id;
            }

        }

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
                NavigationManager.NavigateTo("/DocumentsManagement");
            }
            else
            {
                _ = _message.Error(result.Message);
            }
        }


    }
}
