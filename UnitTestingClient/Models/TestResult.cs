using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestingClient.Models
{
    public class TestResult
    {
        public TestResult() { }

        public TestResult(string name)
        {
            Name = name;
        }

        public TestResult(string name, bool passed)
        {
            Name = name;
            Passed = passed;
        }

        public string Name { get; set; }
        public bool Passed { get; set; }
    }
}
