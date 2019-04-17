﻿using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.Interfaces
{
    public interface ICloudFileService
    {
        Task<IEnumerable<FileCommon>> GetFiles(bool withPublic);
        Task<int> UploadFile(FileCommon fileCommon);
        Task<FileCommon> DownloadFile(FileCommon file);
        Task<bool> DeleteFile(FileCommon file);
        Task<FileCommon> UpdateFileMetadata(FileCommon file);
    }
}
