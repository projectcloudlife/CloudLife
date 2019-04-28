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
        IUserRepository _userRepository;

        public FileService(IFileRepository fileRepo, IUserRepository userRepo)
        {
            _fileRepository = fileRepo;
            _userRepository = userRepo;
        }

        public async Task<bool> DeleteFile(FileCommon file)
        {
            if (await CanAccessFile(file) == false)
            {
                return false;
            }

            var fileToDelete = (await _fileRepository.GetWhere(f => f.Id == file.Id)).First();

            if (fileToDelete.InRecycleBin == false)
            {
                fileToDelete.InRecycleBin = true;
                await _fileRepository.UpdateFile(fileToDelete);
                return true;
            }

            return await _fileRepository.DeleteFile(fileToDelete.Id);
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

        public async Task<IEnumerable<FileCommon>> GetFiles(int userId)
        {
            var files = await _fileRepository.GetWhere(file => file.UserId == userId 
            || (file.IsPublic == true && file.InRecycleBin == false));
            return files;
        }

        async public Task<FileCommon> UpadateFileMetadata(FileCommon file)
        {
            if (await CanAccessFile(file) == false)
            {
                return null;
            }

            var fileToChange = _fileRepository.GetWhere(f => f.Id == file.Id).Result.First();

            fileToChange.InRecycleBin = file.InRecycleBin;
            fileToChange.IsPublic = file.IsPublic;

            return await _fileRepository.UpdateFile(fileToChange.ToDB());
        }

        public async Task<FileCommon> UploadFile(FileCommon file)
        {
            var fileDb = file.ToDB();
            fileDb.UserName = (await _userRepository.Get(file.UserId)).Username;
            fileDb.UploadDate = DateTime.UtcNow;
            var uploadedFileId = await _fileRepository.UploadFile(fileDb);
            return (await _fileRepository.GetWhere(f => f.Id == uploadedFileId)).First().ToCommon();
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
