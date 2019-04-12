using Common.Models;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using Server.Extantions;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class FileService : IFileService
    {
        IFileRepository _fileRepository;

        public FileService(IFileRepository repo)
        {
            using (_fileRepository)
            {
                _fileRepository = repo;
            }
        }

        public async Task<bool> DeleteFile(FileCommon file)
        {
            using (_fileRepository)
            {
                return await _fileRepository.DeleteFile(file.Id);
            }
        }
        /**
         * if file not exist throw exception from repository
         */
        public async Task<FileCommon> DownloadFile(FileCommon file)
        {
            using (_fileRepository)
            {
                return await _fileRepository.DownloadFile(file.Id);
            }
        }

        public async Task<IEnumerable<FileCommon>> GetFiles(bool withPublic)
        {
            using (_fileRepository)
            {
                return await _fileRepository.GetWhere(f => f.IsPublic == withPublic);
            }
        }

        public async Task<int> UploadFile(FileCommon file)
        {
            var fileDb = file.ToDB();
            using (_fileRepository)
            {
                return await _fileRepository.UploadFile(fileDb);
            }
        }
    }
}
