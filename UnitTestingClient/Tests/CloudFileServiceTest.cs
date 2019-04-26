using ClientLogic.Services;
using Common.Models;
using LiveTesting.LogicObjects;
using System.Threading.Tasks;
using UnitTestingClient.MockedClasses;

namespace UnitTestingClient.Tests
{
    public class CloudFileServiceTest : Test
    {

        public CloudFileServiceTest()
        {
            httpService = new HttpService(new MockConfigurationService());
            authService = new AuthService(httpService);

            cloudFileService = new CloudFileService(httpService);
            file = new FileCommon
            {
                Data = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                Name = "file.bin",
                IsPublic = true,
                SizeInBytes = 10
            };
        }

        HttpService httpService;
        AuthService authService;

        CloudFileService cloudFileService;
        FileCommon file;
        int fileId;

        async Task UploadFileTest()
        {
            var user = new AuthInfo { Username = "asdadadasd", Password = "asdadasdsaa" };
            await authService.Register(user);
            await authService.Login(user);

            fileId = await cloudFileService.UploadFile(file);
            file.Id = fileId;

            Assert(fileId > -1);
        }

        //need to run UploadFileTest before.
        async Task DownloadFileTest()
        {
            var res = await cloudFileService.DownloadFile(new FileCommon { Id = fileId });

            Assert(res.SizeInBytes == file.SizeInBytes);
        }

        async Task DeleteFileTest()
        {
            var res = await cloudFileService.DeleteFile(new FileCommon { Id = fileId });

            Assert(res);
        }

        async Task UpdateMetaDataTest()
        {  
            file.IsPublic = false;
            var res = await cloudFileService.UpdateFileMetadata(file);
            Assert(res.IsPublic == false);
        }

    }
}
