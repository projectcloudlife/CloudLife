using ClientLogic.Interfaces;
using Common.Models;
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

        public Task<bool> DeleteFile(FileCommon file)
        {
            return _httpService.Delete<bool>($"api/file/?fileId={file.Id}");
        }

        public Task<FileCommon> DownloadFile(FileCommon file)
        {
            return _httpService.Post<FileCommon, FileCommon>("api/file/download", file);
        }

        public Task<IEnumerable<FileCommon>> GetFiles(bool withPublic)
        {
            return _httpService.Get<IEnumerable<FileCommon>>("api/file");
        }

        public Task<int> UploadFile(FileCommon file)
        {
            return _httpService.Post<int, FileCommon>("api/file/upload", file);
        }

        public Task<FileCommon> UpdateFileMetadata(FileCommon file)
        {
            return _httpService.Put<FileCommon, FileCommon>("api/file", file);
        }
    }
}
