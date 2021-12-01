using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using AntDesign;
using AppFront;
using AppFront.Shared;
using AppFront.Shared._Header;
using AppFront.Shared.ContentParts;
using AppFront.Components;
using AppFront.Services;
using AppFront.Models;
using AppShared.Models;
using BlastCore.Extensions;
using AppFront.Extensions;
using Newtonsoft.Json;
using BlastCore.Features;

namespace AppFront.Components
{
    public partial class FileUpload1
    {
        [Parameter]
        public int MinCount { get; set; } = 0;
        [Parameter]
        public int MaxCount { get; set; } = 1;

        [Parameter]
        public string ActionUrl { get; set; }

        ICollection<FileEntity> _files;

        QServer qclient;

        [Parameter]
        public ICollection<FileEntity> Files
        {
            get => _files;
            set
            {
                if (_files == value) return;

                _files = value;
                SourceChanged(value);

                FilesChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<ICollection<FileEntity>> FilesChanged { get; set; }

        //----------------

        [Inject] public HttpClient _client { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }
        MyJS js;
        //----------------

        Func<UploadFileItem, Task<bool>> _onRemove;

        Dictionary<String, string> headers = new Dictionary<string, string>();
        List<UploadFileItem> fileList = new List<UploadFileItem>();
        [Parameter] public string Accept { get; set; } = ".jpg,.png,.jpeg,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.pdf";

        Dictionary<String, object> upload_data = new Dictionary<string, object>();

        private string _uploadAction;

        [Parameter]
        public Guid ID { get; set; }
        protected override void OnInitialized()
        {
            js = new MyJS(JS);

            headers.Add("Authorization", "Bearer " + Q.AuthToken);
            //this._uploadAction = _client.BaseAddress.ToString() + "api/Zayavka/Upload";
            this._uploadAction = _client.BaseAddress!.ToString() + ActionUrl;

            //_model = new Zayavka();
            //form = new Form<Zayavka>();
            base.OnInitialized();

            //FilesChanged += 

            _ = Load();

            qclient = new QServer(_client);

            _onRemove = f => OnRemove(f);
        }

        public async Task Load()
        {
            if (ID != Guid.Empty)
            {
                //upload_data.Add("id", ID.ToString());

                //var app = await service.Get(ID);
                //_model = app;
                //if (app.Files == null) app.Files = new List<FileEntity>();

                //app.Files.ForEach(f =>
                //{
                //    fileList.Add(f.AsUploadFileItem());
                //});

                //_addNewZayvka = false;
                //StateHasChanged();
            }
        }

        void SourceChanged(ICollection<FileEntity> Files)
        {
            Files.ForEach(f =>
            {
                fileList.Add(f.AsUploadFileItem());
            });
        }

        async void OnSingleCompleted(UploadInfo fileinfo)
        {
            Console.WriteLine("ss");
            if (fileinfo.File.State == UploadState.Success)
            {
                var result = fileinfo.File.GetResponse<ResponseUploadFile>();
                fileinfo.File.Url = result.url;
                //_model.FileId = result.file_id;
                //AppendFile(result);

                Console.WriteLine("ss2");


                //Files.Add(result.as)

                //var t = Task.Run(async () =>
                {
                    string _basePath = "/api/";
                    string _controllerName = "Post";
                    Guid id = result.file_id;
                    var fe = await qclient.GET<FileEntity>($"{_basePath}{_controllerName}/FileEntity/{id}");
                    //var fe = await service.FileEntity(result.file_id);

                    Files.Add(fe);
                    Console.WriteLine("444");
                    _ = FilesChanged.InvokeAsync(Files);
                }
                //});


                //Files.Add(new FileEntity
                //{
                //    Id = result.file_id,

                //})
            }

        }

        void HandleChange(UploadInfo fileinfo)
        {

            Console.WriteLine(JsonConvert.SerializeObject(fileinfo));
        }

        bool BeforeUpload(UploadFileItem uploadFileItem)
        {
            if (ID != Guid.Empty && upload_data.Count < 1)
            {
                upload_data.Add("id", ID.ToString());
            }

            return true;
        }
        async Task<bool> OnRemove(UploadFileItem uploadFileItem)
        {
            ////Console.WriteLine("OnRemove");
            ////Console.WriteLine(JsonConvert.SerializeObject(uploadFileItem));

            ////var result = uploadFileItem.GetResponse<ResponseUploadFile>();
            ////Guid id = result.file_id;
            //Guid id = Guid.Parse(uploadFileItem.Id);

            //var del = await service.DeleteFileEntity(id);

            //if (del)
            //{
            //    var item = Files.First(s => s.Id == id);
            //    Files.Remove(item);

            //}


            return true;
        }
    }
}