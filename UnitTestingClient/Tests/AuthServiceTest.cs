using ClientLogic.Services;
using Common.Enums;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnitTestingClient.MockedClasses;
using UnitTestingClient.Models;

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

        async Task<TestResult> Register()
        {
            var result = new TestResult("AuthService Register", true);


            var response = await authService.Register(authInfo);

            if(response != AuthEnum.Success)
            {
                result.Passed = false;
            }

            var responseBad = await authService.Register(authInfo);

            if(responseBad == AuthEnum.Success)
            {
                result.Passed = false;
            }

            return result;
        }

        //if register test didnt run will fail!!!!
        async Task<TestResult> Login()
        {
            var result = new TestResult("AuthService Login", true);

            var response = await authService.Login(authInfo);

            if(response.AuthResponse != AuthEnum.Success)
            {
                result.Passed = false;
            }

            return result;
        }

    }
}
