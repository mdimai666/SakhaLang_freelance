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

namespace AppFront.Pages.LessonViews
{
    public partial class EditLessonPage
    {
        [Parameter]
        public Guid ID { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public LessonService service { get; set; }
        [Inject] public MessageService _message { get; set; }

        [Inject] public HttpClient _client { get; set; }

        Form<Lesson> form = new Form<Lesson>();

        public Lesson _model = new Lesson();

        //public void OnFinish(EditContext editContext)

        bool _addNewItem = true;

        Dictionary<String, string> headers = new Dictionary<string, string>();
        //string UploadAction;
        List<UploadFileItem> fileList = new List<UploadFileItem>();

        //InputFile inputFile;

        public string Accept = ".jpg,.png,.jpeg,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.pdf";

        BlazoredTextEditor QuillHtml;

        string edit_html { get; set; }

        //public ICollection<FileEntity> Files {get;set; } = new List<FileEntity>();


        protected override void OnInitialized()
        {
            //headers.Add("Authorization", "Bearer " + Q.AuthToken);
            //UploadAction = _client.BaseAddress.ToString() + "api/Document/Upload";
            _model = new Lesson()
            {
                //File = new FileEntity()
                Slug = Guid.NewGuid().ToString(),
            };
            form = new Form<Lesson>();
            base.OnInitialized();

            _ = Load();
        }

        public async Task Load()
        {
            if (ID != Guid.Empty)
            {
                var app = await service.Get(ID);
                //var f = app.File.FileName;
                //var ext = string.IsNullOrEmpty(app.File.FileExt) ? "txt" : app.File.FileExt;
                //Console.WriteLine($"f={f}");
                //fileList.Add(new UploadFileItem
                //{
                //    Id = app.File.Id.ToString(),
                //    FileName = f.Contains(ext) ? f : $"{f}.{ext}",
                //    //Percent = 100,//2
                //    //Ext = "." + app.File.FileExt,//2
                //    //Type = "image/" + app.File.FileExt,//2
                //    //Size = (long)app.File.FileSize,//2
                //    State = UploadState.Success,
                //    Url = app.File.FileUrl,
                //    //ObjectURL = app.File.FileUrl,//2
                //    //Response = JsonConvert.SerializeObject(new ResponseUploadFile(app.File))//2,
                //});
                _model = app;
                edit_html = _model.Content;
                _addNewItem = false;
                StateHasChanged();
            }
        }

        public async Task OnFinish(EditContext editContext)
        {
            Lesson a;
            _model.Content = await QuillHtml.GetHTML();

            a = await service.SmartSave(_addNewItem, _model);


            if (a is not null && _addNewItem)
            {
                //_ = _message.Success("Сохранено");
                NavigationManager.NavigateTo($"/Lesson/Edit/{a.Id}", replace: true);
                _addNewItem = false;
                _ = Load();
            }
            StateHasChanged();
        }

        public void OnChangeFile(IReadOnlyList<IBrowserFile> files)
        {
            Console.WriteLine("12");
        }

        public async void OnDeleteClick()
        {
            var result = await service.Delete(_model.Id);

            if (result.Ok == true)
            {
                _ = _message.Success(result.Message);
                NavigationManager.NavigateTo("/NewsManagement");
            }
            else
            {
                _ = _message.Error(result.Message);
            }
        }

        void OnFileUpload(FileEntity fileEntity)
        {
            if (fileEntity.FileGroup == "Image")
            {
                _model.Image = fileEntity.FileUrl;
            }
        }
        void OnDeleteFile(FileEntity fileEntity)
        {
            if (fileEntity.FileGroup == "Image")
            {
                _model.Image = "";
            }
        }


    }
}
