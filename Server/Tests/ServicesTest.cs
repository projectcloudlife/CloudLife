using Common.Enums;
using Common.Models;
using LiveTesting.LogicObjects;
using Server.DAL.Interfaces;
using Server.Extantions;
using Server.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Tests
{
    public class ServicesTest : Test
    {

        IAuthService _authService;
        IFileService _fileService;
        ITokenGeneratorService _tokenGeneratorService;
        IUserRepository _userRepository;
        AuthInfo _Goodinfo;
        AuthInfo _UsernameTakenRegister;
        AuthInfo _BadUsernameLogin;
        AuthInfo _BadPasswordRegister;
        AuthInfo _BadPasswordLogin;

        public ServicesTest(IAuthService authService, IUserRepository userRepository, IFileService fileService, ITokenGeneratorService tokenService)
        {
            _authService = authService;
            _fileService = fileService;
            _userRepository = userRepository;
            _tokenGeneratorService = tokenService;
            _Goodinfo = new AuthInfo { Username = "assaf", Password = "123456" };
            _UsernameTakenRegister = new AuthInfo { Username = "assaf", Password = "1" };
            _BadUsernameLogin = new AuthInfo { Username = "assafdasas", Password = "1" };
            _BadPasswordRegister = new AuthInfo { Username = "assaf1", Password = "1" };
            _BadPasswordLogin = new AuthInfo { Username = "assaf", Password = "1" };
        }

        async Task RegisterTest()
        {
            var goodResult = await _authService.Register(_Goodinfo);

            var badUserResult = await _authService.Register(_Goodinfo);

            var BadPasswordResult = await _authService.Register(_BadPasswordRegister);

            Assert(goodResult == AuthEnum.Success &&
                   BadPasswordResult == AuthEnum.BadPassword &&
                   badUserResult == AuthEnum.BadUsername);

        }

        async Task LoginTest()
        {
            var info = new AuthInfo { Username = "assaf1", Password = "123456" };

            await _authService.Register(info);

            var result = await _authService.Login(info);

            var badUserResult = await _authService.Login(_BadUsernameLogin);

            var badPasswordResult = await _authService.Login(_BadPasswordLogin);

            Assert(result.AuthResponse == AuthEnum.Success &&
                   result.Token != "" &&
                   badUserResult.AuthResponse == AuthEnum.BadUsername &&
                   badPasswordResult.AuthResponse == AuthEnum.BadPassword);
        }

        async Task GetByIdTest()
        {
            var info = new AuthInfo { Username = "assaf12", Password = "123456" };
            var info2 = new AuthInfo { Username = "assa", Password = "123456" };

            await _authService.Register(info);

            var id = await _userRepository.GetId(info);
            var id2 = await _userRepository.GetId(info);
            var id3 = await _userRepository.GetId(info2);
        }

        public async Task FilesTest()
        {
            var info = new AuthInfo { Username = "assaf2", Password = "123456" };

            await _authService.Register(info);

            var userId = await _userRepository.GetId(info);

            var fileList = await _fileService.GetFiles(userId);

            var begin = fileList.ToList().Count;

            var file = new FileCommon
            {
                IsPublic = true,
                InRecycleBin = false,
                Name = "file.exe",
                SizeInBytes = 21323,
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                UserId = userId
            };

            await _fileService.UploadFile(file);

            fileList = await _fileService.GetFiles(userId);

            var end = fileList.ToList().Count;

            Assert(end - begin == 1);
        }

    }
}
