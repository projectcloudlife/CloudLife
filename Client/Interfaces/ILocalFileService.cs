using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface ILocalFileService
    {
        Task<IEnumerable<FileCommon>> SelectFiles();
        Task<bool> SaveFile(FileCommon fileCommon);
        Task<FileCommon> GetFileData(FileCommon fileCommon);
    }
}
