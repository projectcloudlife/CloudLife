using ClientLogic.Services;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitTestingClient.MockedClasses;
using UnitTestingClient.Models;

namespace UnitTestingClient.Tests
{
    public class CloudFileServiceTest : Test
    {

        public CloudFileServiceTest()
        {
            Init().Wait();
        }

        async Task Init()
        {
            var httpService = new HttpService(new MockConfigurationService());
            var authService = new AuthService(httpService);
            var user = new AuthInfo { Username = "asdadadasd", Password = "asdadasdsaa" };
            await authService.Register(user);
            await authService.Login(user);
            cloudFileService = new CloudFileService(httpService);
            file = new FileCommon
            {
                Data = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                Name = "file.bin",
                IsPublic = true,
                SizeInBytes = 10
            };
        }

        CloudFileService cloudFileService;
        FileCommon file;
        int fileId;

        async Task<TestResult> UploadFileTest()
        {
            var result = new TestResult("FileService Upload File");
            fileId = await cloudFileService.UploadFile(file);

            result.Passed = fileId > -1;

            return result;
        }

        //need to run UploadFileTest before.
        async Task<TestResult> DownloadFileTest()
        {
            var result = new TestResult("CloudFileService Download File", true);

            var res = await cloudFileService.DownloadFile(new FileCommon { Id = fileId });

            if(res.SizeInBytes != file.SizeInBytes)
            {
                result.Passed = false;
            }

            return result;
        }

        async Task<TestResult> DeleteFileTest()
        {
            var result = new TestResult("CloudFileService Delete File", true);

            var res = await cloudFileService.DeleteFile(new FileCommon { Id = fileId });

            result.Passed = res;

            return result;
        }

    }
}
