using Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.Interfaces
{
    public interface ICloudFileService
    {
        event Action<FileCommon> FileUploaded;
        event Action<FileCommon> FileMetaDataChanged;
        Task<IEnumerable<FileCommon>> GetFiles();
        Task<FileCommon> UploadFile(FileCommon fileCommon);
        Task<FileCommon> DownloadFile(FileCommon file);
        Task<bool> DeleteFile(FileCommon file);
        Task<FileCommon> UpdateFileMetadata(FileCommon file);
    }
}
