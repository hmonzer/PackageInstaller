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
        public void Contains_returns_false_if_PackageDefinition_does_not_exist()
        {
            Graph graph = new Graph();
            PackageDefinition definition = new PackageDefinition("A");
            Assert.IsFalse(graph.Contains(definition.Package));
        }
        

        [Test]
        public void AddNode_adds_node_into_list()
        {
            Graph graph = new Graph();
            PackageDefinition definition = new PackageDefinition("A");
            graph.AddPackageDefinition(definition);
            Assert.IsTrue(graph.Contains(definition.Package));
        }

        [Test]
        public void SortTopologically_returns_the_node_if_it_has_no_dependency_list()
        {
            Graph graph = new Graph();
            PackageDefinition definition = new PackageDefinition("A");
            graph.AddPackageDefinition(definition);
            Queue<string> sortedQueue = graph.SortTopologically();
            Assert.IsTrue(sortedQueue.Count == 1);
            Assert.AreEqual(sortedQueue.Peek(), definition.Package);
        }

        [Test]
        public void SortTopologically_returns_dependency_package_before_the_package_itself()
        {
            Graph graph = new Graph();
            PackageDefinition definitionA = new PackageDefinition("A:B");
            PackageDefinition definitionB = new PackageDefinition("B");
            graph.AddPackageDefinition(definitionA);
            graph.AddPackageDefinition(definitionB);
            Queue<string> sortedQueue = graph.SortTopologically();
            Assert.AreEqual(sortedQueue.Count, 2);
            Assert.AreEqual(sortedQueue.Dequeue(), definitionB.Package);
            Assert.AreEqual(sortedQueue.Dequeue(), definitionA.Package);
        }


        
    }
}
