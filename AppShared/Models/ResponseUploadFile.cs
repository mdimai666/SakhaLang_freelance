using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    public class ResponseUploadFile
    {
        public string name { get; set; }
        public string status { get; set; }
        public string url { get; set; }
        public string url_rel { get; set; }
        public string thumbUrl { get; set; }
        public Guid file_id { get; set; }

        public ResponseUploadFile()
        {

        }

        public ResponseUploadFile(FileEntity fileEntity)
        {
#if DEBUG
            string domain = "http://localhost:5833"; 
#else
            string domain = "https://dev.do.sakha.education:81";
#endif

            name = fileEntity.FileName;
            status = "success";
            url = domain + fileEntity.FileUrl.Replace(domain, "");
            thumbUrl = domain + fileEntity.FileUrl.Replace(domain, "");
            file_id = fileEntity.Id;
            url_rel = fileEntity.FileUrl.Replace(domain, "");
        }
    }
}
