using Server.DAL.Interfaces;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {

        public FileRepository(ContextDev context)
        {
            _context = context;
        }

        ContextDev _context;

        public async Task<bool> DeleteFile(int fileId)
        {
            var file = _context.Files.FirstOrDefault(fileDb => fileDb.Id == fileId);

            if (file == null)
            {
                return false;
            }

            if (file.InRecycleBin == false)
            {
                file.InRecycleBin = true;
                _context.Files.Update(file);
                await _context.SaveChangesAsync();
                return true;
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<FileDB> DownloadFile(int fileId)
        {
            return Task.Run(() =>
            {
                var file = _context.Files.FirstOrDefault(fileDb => fileDb.Id == fileId);

                if (file == null)
                {
                    throw new Exception($"File with id {fileId} doesn't exists.");
                }

                return file;
            });
        }

        public Task<IEnumerable<FileDB>> GetWhere(Func<FileDB, bool> expr)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<FileDB>)_context.Files.Where(expr).ToList();
            });
        }

        public async Task<int> UploadFile(FileDB file)
        {
            var newFileEntry = _context.Files.Add(file);
            await _context.SaveChangesAsync();
            return newFileEntry.Entity.Id;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
