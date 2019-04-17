using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

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
                var isTestMethod = method.ReturnType == typeof(TestResult) || 
                                   method.ReturnType == typeof(Task<TestResult>);
                return isTestMethod;
            });

            foreach (var method in methods)
            {
                testMethods.Add(() =>
                {
                    if(method.ReturnType == typeof(Task<TestResult>))
                    {
                        return ((Task<TestResult>)method.Invoke(this, null)).Result;
                    }

                    return (TestResult)method.Invoke(this, null);
                });
            }
        }

        protected void Assert(bool passed)
        {

        }

        public IEnumerable<TestResult> RunTest()
        {
            return testMethods.Select(method => method());
        }

    }
}
