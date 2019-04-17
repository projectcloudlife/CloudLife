using ClientLogic.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
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

        public Task<bool> UploadFile(FileCommon file)
        {
            return _httpService.Post<bool, FileCommon>("api/file/upload", file);
        }
    }
}
