using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PackageInstaller.Tests
{
    [TestFixture]
    public class PackageDefinitionTests
    {
        [Test, TestCaseSource("packageTestData")]
        public void given_a_string_constructs_a_correct_PackageNode(string input, string package,
            List<string> dependencies)
        {
            PackageDefinition packageDef = new PackageDefinition(input);
            Assert.AreEqual(packageDef.Package, package);
            Assert.IsFalse(packageDef.Dependencies.Except(dependencies).Any());

        }

        public IEnumerable<TestCaseData> packageTestData
        {
            get
            {
                Collection<TestCaseData> testCases = new Collection<TestCaseData>();

                TestCaseData noDepdencyCase = new TestCaseData("A", "A", new List<string>());
                testCases.Add(noDepdencyCase);
                TestCaseData singleDepdencyCase = new TestCaseData("A: B", "A", new List<string>() {"B"});
                testCases.Add(singleDepdencyCase);
                TestCaseData multipleDependencyCase = new TestCaseData("A: B, C, D", "A",
                    new List<string>() {"B", "C", "D"});
                testCases.Add(multipleDependencyCase);

                return testCases;
            }
        }
    }
}
