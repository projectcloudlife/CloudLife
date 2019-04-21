using ClientLogic.Interfaces;
using ClientLogic.Models;
using Common.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace Client.Services
{
    public class LocalFileService : ILocalFileService
    {
        async public Task<FileCommon> GetFileWithData(FileClient file)
        {
            var storageFile = await StorageFile.GetFileFromPathAsync($"{file.Path}/{file.Name}");
            var buffer = await FileIO.ReadBufferAsync(storageFile);
            var reader = DataReader.FromBuffer(buffer);
            var data = new byte[buffer.Length];
            reader.ReadBytes(data);

            return new FileCommon()
            {
                Name = storageFile.Name,
                Data = data,
                SizeInBytes = (int)buffer.Length
            };
        }

        async public Task<bool> SaveFile(FileClient fileClient)
        {
            var folder = await StorageFolder.GetFolderFromPathAsync(fileClient.Path);
            var file = await folder.CreateFileAsync(fileClient.Name);

            using (var writer = await file.OpenStreamForWriteAsync())
            {
                await writer.WriteAsync(fileClient.Data, 0, fileClient.SizeInBytes);
            }

            return true;
        }

        async public Task<IEnumerable<FileClient>> SelectFiles()
        {
            var filePicker = new FileOpenPicker();
            filePicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            filePicker.FileTypeFilter.Add("*");

            var storageFiles = await filePicker.PickMultipleFilesAsync();
            
            var files = new ConcurrentBag<FileClient>();

            //storageFiles.AsParallel().ForAll(async file =>
            //{
            //    var fileClient = await ToFileClient(file);
            //    files.Add(fileClient);
            //});

            foreach (var file in storageFiles)
            {
                var fileClient = await ToFileClient(file);
                files.Add(fileClient);
            }

            return files;
        }


        async Task<FileClient> ToFileClient(StorageFile file)
        {
            FileClient fileClient = new FileClient()
            {
                Name = file.Name,
                Path = file.Path
            };

            var props = await file.GetBasicPropertiesAsync();
            fileClient.SizeInBytes = (int)props.Size;

            return fileClient;
        }

        async public Task<string> SelectFolder()
        {
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            var folder = await folderPicker.PickSingleFolderAsync();
            return folder.Path;
        }
    }
}
