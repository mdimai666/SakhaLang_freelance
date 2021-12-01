using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace AppShared.Models
{
    public class FileEntity : BasicEntity
    {

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public ulong FileSize { get; set; }
        public string FileExt { get; set; }
        public EFileType FileType { get; set; }

        public string FileGroup { get; set; }


#if DEBUG
        string domain = "http://localhost:5833";
#else
        string domain = "https://dev.do.sakha.education:81/";
#endif

        [NotMapped]
        public string FileUrl => Path.Join(domain, "upload", FilePath?.Trim('/') ?? "").Replace("\\", "/");
        [NotMapped]
        public bool IsImage => ExtIsImage(FileExt);

        public FileEntity()
        {

        }


        //public FileEntity(ResponseUploadFile fileinfo)
        //{

        //}

        const string ic_pdf = "/img/docs/pdf.png";
        const string ic_doc = "/img/docs/doc.png";
        const string ic_xls = "/img/docs/xls.png";
        const string ic_img = "/img/docs/img.png";
        const string ic_unknown = "/img/docs/unknown.png";

        public static string ExtPreviewUrl(string ext)
        {
            switch (ext.Trim('.'))
            {
                case "img":
                    return ic_img;
                case "pdf":
                    return ic_pdf;
                case "doc":
                case "docx":
                    return ic_doc;
                case "xls":
                case "xlsx":
                    return ic_xls;
                default:
                    return ic_unknown;
            }
        }

        public const string imgExtList = "jpg,png,jpeg,ico,gif,";

        public static bool ExtIsImage(string ext)
        {
            if (string.IsNullOrEmpty(ext)) return false;
            return imgExtList.Contains(ext.ToLower() + ",");
        }

    }

    public enum EFileType
    {
        None,
        UserAvatar,
        ApplicationAttachment,
        Document,
        PostAttachment,
        CategoryAttachment,
        LessonAttachment,


    }
}