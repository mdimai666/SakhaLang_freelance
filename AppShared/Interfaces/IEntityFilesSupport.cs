using AppShared.Models;
using System.Collections.Generic;

namespace AppShared.Interfaces
{
    public interface IEntityFilesSupport
    {
        public ICollection<FileEntity> FileList { get; set; }

    }
}
