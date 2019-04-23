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

        async Task<TestResult> UploadFileTest()
        {
            var user = new AuthInfo { Username = "asdadadasd", Password = "asdadasdsaa" };
            await authService.Register(user);
            await authService.Login(user);

            var result = new TestResult("FileService Upload File");
            fileId = await cloudFileService.UploadFile(file);
            file.Id = fileId;
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

        async Task<TestResult> UpdateMetaData()
        {
            var result = new TestResult("CloudFileService UpdateMetaData", true);
            file.IsPublic = false;
            var res = await cloudFileService.UpdateFileMetadata(file);
            if (res.IsPublic == true)
            {
                result.Passed = false;
            }
            return result;
        }

    }
}
