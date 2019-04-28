using ClientLogic.Models;
using Common.Models;

namespace Client.Extensions
{
    public static class ClientExtension
    {
        public static FileCommon ToCommon(this FileClient file)
        {
            return new FileCommon()
            {
                Data = file.Data,
                Id = file.Id,
                InRecycleBin = file.InRecycleBin,
                IsPublic = file.IsPublic,
                Name = file.Name,
                SizeInBytes = file.SizeInBytes,
                UserId = file.UserId,
                UploadDate = file.UploadDate
                
            };
        }

        public static void UpdateMetadata(this FileCommon file, FileCommon fileToCopy)
        {
            file.InRecycleBin = fileToCopy.InRecycleBin;
            file.IsPublic = fileToCopy.IsPublic;
        }

        public static FileClient ToClient(this FileCommon file)
        {
            return new FileClient()
            {
                Data = file.Data,
                Id = file.Id,
                InRecycleBin = file.InRecycleBin,
                IsPublic = file.IsPublic,
                Name = file.Name,
                SizeInBytes = file.SizeInBytes,
                UserId = file.UserId,
                UploadDate = file.UploadDate
            };
        }
    }
}
