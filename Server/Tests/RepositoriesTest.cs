using LiveTesting.LogicObjects;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Tests
{
    public class RepositoriesTest : Test
    {

        public RepositoriesTest(IUserRepository userRepository, IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
        }

        IFileRepository _fileRepository;
        IUserRepository _userRepository;

        async Task UserRepoTest()
        {
            Random rnd = new Random();
            var username = "username344" + rnd.Next(100000).ToString();

            //Create user
            var user = await _userRepository.Create(new UserDB()
            {
                Username = username,
                Password = "asdsadasasdas"

            });

            var getUser = await _userRepository.Get(user.Id);

            Assert(getUser.Username == username);
        }

        async Task FileRepoTest()
        {
            Random rnd = new Random();
            var username = "username123";

            var user = await _userRepository.Create(new UserDB()
            {
                Username = username,
                Password = "1dqwdsad"
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

            //Delete File
            var deleted = await _fileRepository.DeleteFile(fileId);
        }


    }
}
