using ClientLogic.Services;
using Common.Enums;
using Common.Models;
using LiveTesting.LogicObjects;
using System.Threading.Tasks;
using UnitTestingClient.MockedClasses;

namespace UnitTestingClient.Tests
{
    public class AuthServiceTest : Test
    {

        public AuthServiceTest()
        {
            authService = new AuthService(new HttpService(new MockConfigurationService()));
            authInfo = new AuthInfo { Username = "dasdad", Password = "asdfasdfa" };
        }

        AuthService authService;
        AuthInfo authInfo;

        async Task RegisterTest()
        {
            var response = await authService.Register(authInfo);

            Assert(response == AuthEnum.Success);

            var responseBad = await authService.Register(authInfo);

            Assert(responseBad != AuthEnum.Success);
        }

        //if register test didnt run will fail!!!!
        async Task LoginTest()
        {
            var response = await authService.Login(authInfo);

            Assert(response.AuthResponse == AuthEnum.Success);
        }

    }
}
