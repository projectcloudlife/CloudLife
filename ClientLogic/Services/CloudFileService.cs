using ClientLogic.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.Services
{
    public class CloudFileService : ICloudFileService
    {

        public CloudFileService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        IHttpService _httpService;

        public event Action<FileCommon> FileUploaded;
        public event Action<FileCommon> FileMetaDataChanged;

        public Task<bool> DeleteFile(FileCommon file)
        {
            return _httpService.Delete<bool>($"api/file/?fileId={file.Id}");
        }

        public Task<FileCommon> DownloadFile(FileCommon file)
        {
            return _httpService.Post<FileCommon, FileCommon>("api/file/download", file);
        }

        public Task<IEnumerable<FileCommon>> GetFiles()
        {
            return _httpService.Get<IEnumerable<FileCommon>>("api/file");
        }

        public async Task<FileCommon> UploadFile(FileCommon file)
        {
            var uploadedFile = await _httpService.Post<FileCommon, FileCommon>("api/file/upload", file);

            FileUploaded?.Invoke(uploadedFile);

            return uploadedFile;
        }

        public async Task<FileCommon> UpdateFileMetadata(FileCommon file)
        {
            var fileUpdated = await _httpService.Put<FileCommon, FileCommon>("api/file", file);
            FileMetaDataChanged(fileUpdated);
            return fileUpdated;
        }
    }
}
