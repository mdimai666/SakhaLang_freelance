using AntDesign;
using AppShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFront.Extensions
{
    public static class ExtensionFileEntity
    {
        public static UploadFileItem AsUploadFileItem(this FileEntity fileEntity)
        {
            var fn = fileEntity.FileName;
            var ext = string.IsNullOrEmpty(fileEntity.FileExt) ? "unknown" : fileEntity.FileExt;
            
            return new UploadFileItem
            {
                Id = fileEntity.Id.ToString(),
                FileName = fn.Contains(ext) ? fn : $"{fn}.{ext}",
                //Percent = 100,//2
                //Ext = "." + app.File.FileExt,//2
                //Type = "image/" + app.File.FileExt,//2
                //Size = (long)app.File.FileSize,//2
                State = UploadState.Success,
                Url = fileEntity.FileUrl,
                //ObjectURL = app.File.FileUrl,//2
                //Response = JsonConvert.SerializeObject(new ResponseUploadFile(app.File))//2,
            };
        }
    }
}
