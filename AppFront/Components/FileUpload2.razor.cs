using AntDesign;
using AppFront.Extensions;
using AppFront.Services;
using AppShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace AppFront.Components
{
    public partial class FileUpload2
    {
        [Parameter] public int MinCount { get; set; } = 0;
        [Parameter] public int MaxCount { get; set; } = 1;

        [Parameter] public string ActionUrl { get; set; }

        [Parameter] public bool ReadOnly { get; set; } = false;
        [Inject] public ConfirmService _confirmService { get; set; }

        [Parameter] public string ViewFiltergroup { get; set; } = null;


        ICollection<FileEntity> _files;

        public Upload uploadref;

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

        public IEnumerable<FileEntity> FilesFilteredSorted
        {
            get
            {
                if (!string.IsNullOrEmpty(ViewFiltergroup))
                {
                    return Files?.Where(s => s.FileGroup == ViewFiltergroup).OrderBy(s => s.Created);
                }

                return Files?.OrderBy(s => s.Created);

            }
        }

        //----------------

        [Inject] public PostService service { get; set; }
        [Inject] public MessageService _message { get; set; }
        [Inject] public HttpClient _client { get; set; }

        [Inject] IJSRuntime JS { get; set; }
        MyJS js;
        //----------------

        Func<UploadFileItem, Task<bool>> _onRemove;

        Dictionary<String, string> headers = new Dictionary<string, string>();
        List<UploadFileItem> fileList = new List<UploadFileItem>();
        [Parameter] public string Accept { get; set; } = ".jpg,.png,.jpeg,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.pdf";

        Dictionary<String, object> upload_data = new Dictionary<string, object>();

        private string _uploadAction;

        [Parameter] public EventCallback<FileEntity> OnFileUpload { get; set; }
        [Parameter] public EventCallback<FileEntity> OnDeleteFile { get; set; }

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

            _onRemove = f => OnRemove(f);
        }

        public async Task Load()
        {
            if (ID != Guid.Empty)
            {
            }
        }

        void SourceChanged(ICollection<FileEntity> Files)
        {
            Files.ForEach(f =>
            {
                fileList.Add(f.AsUploadFileItem());
            });
        }

        void OnSingleCompleted(UploadInfo fileinfo)
        {
            if (fileinfo.File.State == UploadState.Success)
            {
                var result = fileinfo.File.GetResponse<ResponseUploadFile>();
                fileinfo.File.Url = result.url;
                //_model.FileId = result.file_id;
                //AppendFile(result);


                Task.Run(async () =>
                {
                    var fe = await service.FileEntity(result.file_id);
                    Files.Add(fe);
                    _ = OnFileUpload.InvokeAsync(fe);
                    StateHasChanged();
                });


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
            //Console.WriteLine("OnRemove");
            //Console.WriteLine(JsonConvert.SerializeObject(uploadFileItem));

            //var result = uploadFileItem.GetResponse<ResponseUploadFile>();
            //Guid id = result.file_id;
            Guid id = Guid.Parse(uploadFileItem.Id);

            var del = await service.DeleteFileEntity(id);

            if (del.Ok)
            {
                var item = Files.First(s => s.Id == id);
                Files.Remove(item);
                _ = OnDeleteFile.InvokeAsync(item);
            }


            return true;
        }
        async Task<bool> OnRemove(FileEntity fileEntity)
        {
            Guid id = fileEntity.Id;

            var del = await service.DeleteFileEntity(id);

            if (del.Ok)
            {
                var item = Files.First(s => s.Id == id);
                Files.Remove(item);
                _ = OnDeleteFile.InvokeAsync(item);
            }


            return true;
        }

        async Task<bool> ItemRemoveClick(UploadFileItem uploadFileItem)
        {
            var result = await OnRemove(uploadFileItem);

            if (result)
            {
                fileList.Remove(uploadFileItem);
            }

            return true;
        }
        async void ItemRemoveClick(FileEntity uploadFileItem)
        {
            var confirm = await _confirmService.Show("Потдтвердите действие", "Уверены что хотите удалить?", ConfirmButtons.OKCancel, ConfirmIcon.Warning);

            if (confirm == ConfirmResult.OK)
            {

                var result = await OnRemove(uploadFileItem);

                if (result)
                {
                    var file = fileList.First(s => s.Id == uploadFileItem.Id.ToString());
                    fileList.Remove(file);
                    _ = OnDeleteFile.InvokeAsync(uploadFileItem);
                }
                StateHasChanged();
            }
        }
    }
}