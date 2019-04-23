using System;
using System.Collections;
using System.Collections.Generic;
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
                IEnumerable<TestResult> results = null;
                    results = test.RunTest();

                if (results != null)
                {
                    foreach (var result in results)
                    {
                        PrintTestResult(result);
                    }
                }

            }

        }

        public static void PrintTestResult(TestResult testResult)
        {
            var passedString = testResult.Passed ? "Passed" : "Failed";
            var Color = testResult.Passed ? ConsoleColor.Green : ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{testResult.Name}: ");
            Console.ForegroundColor = Color;
            Console.WriteLine($"{passedString}");
            Console.ForegroundColor = ConsoleColor.White;

        }

    }
}
