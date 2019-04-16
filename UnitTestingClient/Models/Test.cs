using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using UnitTestingClient.Attributes;

namespace UnitTestingClient.Models
{
    public class Test
    {
        public string Name { get; set; }
        List<Func<TestResult>> testMethods = new List<Func<TestResult>>();
        
        public Test()
        {
            var methods = GetType().GetRuntimeMethods().Where(method =>
            {
                var isTestMethod = method.GetCustomAttribute(typeof(TestAttribute)) != null;
                return isTestMethod;
            });

            foreach (var method in methods)
            {
                testMethods.Add(() =>
                {
                    return (TestResult)method.Invoke(this, null);
                });
            }
        }

        public IEnumerable<TestResult> RunTest()
        {
            return testMethods.Select(method => method());
        }

    }
}
