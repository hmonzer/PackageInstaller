using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PackageInstaller.Tests
{
    [TestFixture]
    public class PackageDependencyResolverTests
    {
        [Test, TestCaseSource("AcceptanceTestCases")]
        public void Acceptance_test(string[] packages, string output)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                PackageDependencyResolver.Main(packages);
                Assert.AreEqual(output, sw.ToString());
            }
        }

        public IEnumerable<TestCaseData> AcceptanceTestCases
        {
            get
            {
                Collection<TestCaseData> cases = new Collection<TestCaseData>();
                string[] validPackages = new[]
                {
                    "KittenService:", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService",
                    "Fraudstream: Leetmeme", "Ice:"
                };
                string validOutput = "KittenService, Ice, Cyberportal, Leetmeme, CamelCaser, Fraudstream";
                TestCaseData validData = new TestCaseData(validPackages, validOutput);
                validData.SetName("Valid Case");
                cases.Add(validData);

                string[] InvalidPackages = new[]
                {
                    "KittenService:", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService",
                    "Fraudstream:", "Ice: Leetmeme"
                };
                TestCaseData invalidData = new TestCaseData(InvalidPackages, null).Throws(typeof(InvalidOperationException));
                cases.Add(invalidData);
                invalidData.SetName("Invalid Case");

                return cases;
            }
        } 
    }
}
