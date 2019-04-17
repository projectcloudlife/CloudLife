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
            _fileRepository = repo;
        }

        public async Task<bool> DeleteFile(FileCommon file)
        {
            if (await CanAccessFile(file) == false)
                return false;

            if (file.InRecycleBin == false)
            {
                file.InRecycleBin = true;
                await _fileRepository.UpdateFile(file.ToDB());
                return true;
            }

            return await _fileRepository.DeleteFile(file.Id);
        }
        /**
         * if file not exist throw exception from repository
         */
        public async Task<FileCommon> DownloadFile(FileCommon file)
        {
            if (await CanAccessFile(file) == false)
                return null;

            return await _fileRepository.DownloadFile(file.Id);
        }

        public async Task<IEnumerable<FileCommon>> GetFiles(int userId, bool withPublic)
        {
            return await _fileRepository.GetWhere(file => file.UserId == userId && file.IsPublic == withPublic );
        }

        public async Task<int> UploadFile(FileCommon file)
        {
            var fileDb = file.ToDB();
            return await _fileRepository.UploadFile(fileDb);
        }

        async Task<bool> CanAccessFile(FileCommon file)
        {
            var fileDb = (await _fileRepository.GetWhere(fileGw => fileGw.Id == file.Id)).FirstOrDefault();

            if (fileDb == null)
                return false;

            if (file.UserId != file.UserId)
                return false;

            return true;
        }

    }
}
