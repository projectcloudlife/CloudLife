using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace UnitTestingClient.Models
{
    public static class TestRunner
    {

        public static void RunTests()
        {

            var assembly = typeof(TestRunner).Assembly;

            var testTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Test)));

            foreach (var testType in testTypes)
            {
                var test = (Test)Activator.CreateInstance(testType);
                var results = test.RunTest();

                foreach (var result in results)
                {
                    PrintTestResult(result);
                }
            }

        }

        public static void PrintTestResult(TestResult testResult)
        {
            var passedString = testResult.Passed ? "Passed" : "failed";
            Console.WriteLine($"{testResult.Name}: {passedString}");
        }

    }
}
