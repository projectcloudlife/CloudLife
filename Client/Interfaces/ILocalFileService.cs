using Client.Models;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface ILocalFileService
    {
        Task<IEnumerable<FileClient>> SelectFiles();
        Task<bool> SaveFile(FileClient fileCommon);
        Task<string> SelectFolder();
        Task<FileCommon> GetFileWithData(FileClient file);
    }
}
