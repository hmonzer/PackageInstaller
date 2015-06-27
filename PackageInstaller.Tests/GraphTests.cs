using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PackageInstaller.Tests
{
    public class GraphTests
    {
        [Test]
        public void AddNode_adds_node_into_list()
        {
            Graph graph = new Graph();
            PackageDefinition definition = new PackageDefinition("A");
            graph.AddPackageDefinition(definition);
            Assert.IsTrue(graph.Contains(definition.Package));
        }
    }
}
