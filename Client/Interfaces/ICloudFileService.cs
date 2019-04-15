using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface ICloudFileService
    {
        Task<IEnumerable<FileCommon>> GetFiles(bool withPublic);
        Task<bool> UploadFile(FileCommon fileCommon);
        Task<FileCommon> DownloadFile(FileCommon file);
        Task<bool> DeleteFile(FileCommon file);
    }
}
