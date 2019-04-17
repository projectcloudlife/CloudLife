using System;
using System.Collections.Generic;
using System.Text;
using UnitTestingClient.Attributes;
using UnitTestingClient.Models;

namespace UnitTestingClient.Tests
{
    public class TestForTest : Test
    {

        [Test]
        public TestResult RandomName()
        {
            return new TestResult { Name = "Test", Passed = true };
        }

    }
}
