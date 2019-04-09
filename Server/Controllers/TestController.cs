using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Interfaces;
using Server.DAL.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public TestController(IUserRepository userRepository,IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
        }

        IFileRepository _fileRepository;
        IUserRepository _userRepository;

        public async Task<ActionResult<string>> Test()
        {

            var userRepoResult = (await UserRepoTest()).Value;
            var fileRepoResult = (await FileRepoTest()).Value;

            var html = $"User Repository Test: {userRepoResult}\n" +
                       $"file Repository Test: {fileRepoResult}";

            return html;
        }

        [HttpGet]
        [Route("userrepo")]
        public async Task<ActionResult<bool>> UserRepoTest()
        {
            var username = "username";

            //Create user
            var user = await _userRepository.Create(new UserDB()
            {
                AuthInfo = new AuthInfo()
                {
                    Username = username,
                    Password = ""
                }
            });

            var getUser = await _userRepository.Get(user.Id);

            if(getUser.AuthInfo.Username != username)
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        [Route("filerepo")]
        public async Task<ActionResult<bool>> FileRepoTest()
        {
            var user = await _userRepository.Create(new UserDB()
            {
                AuthInfo = new AuthInfo()
                {
                    Username = "",
                    Password = ""
                }
            });

            var fileName = "file1.exe";
         
            //Upload file
            var fileId = await _fileRepository.UploadFile(new FileDB()
            {
                Name = fileName,
                UserId = user.Id
            });

            //Download File
            var file = await _fileRepository.DownloadFile(fileId);

            //Fileter
            var files = await _fileRepository.GetWhere(fileDb => fileDb.Name == fileName);

            //
            var deleted = await _fileRepository.DeleteFile(fileId);

            return true;
        }


    }
}