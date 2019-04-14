using Microsoft.EntityFrameworkCore;
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
        bool disposed = false;

        public FileRepository(ContextDev context)
        {
            _context = context;
        }

        ContextDev _context;

        public async Task<bool> DeleteFile(int fileId)
        {
            var file = _context.Files.FirstOrDefault(fileDb => fileDb.Id == fileId);

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
            _context.SaveChanges();
            return newFileEntry.Entity.Id;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        async public Task<FileDB> UpdateFile(FileDB file)
        {
            var fileToUpdate = _context.Files.Find(file.Id);
            var entry = _context.Entry(fileToUpdate);
            entry.CurrentValues.SetValues(file);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return fileToUpdate;
        }
    }
}
