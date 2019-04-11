using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Server.Attributes;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using Server.Extantions;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [TestController]
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

        [HttpGet]
        [Route("userrepo")]
        public async Task<ActionResult<bool>> UserRepoTest()
        {
            Random rnd = new Random();
            var username = "username" + rnd.Next(100000).ToString();

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
            Random rnd = new Random();
            var username = "username" + rnd.Next(100000).ToString();

            var user = await _userRepository.Create(new UserDB()
            {
                AuthInfo = new AuthInfo()
                {
                    Username = username,
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