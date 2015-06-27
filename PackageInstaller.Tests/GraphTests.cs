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
            Assert.AreEqual(definition.Package, sortedQueue.Peek());
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
            Assert.AreEqual(2, sortedQueue.Count);
            Assert.AreEqual(definitionB.Package, sortedQueue.Dequeue());
            Assert.AreEqual(definitionA.Package, sortedQueue.Dequeue());
        }

        [Test]
        public void SortTopologically_returns_dependency_package_before_the_package_itself_regardless_of_addition_order_to_graph()
        {
            Graph graph = new Graph();
            PackageDefinition definitionB = new PackageDefinition("B");
            PackageDefinition definitionA = new PackageDefinition("A:B");
            graph.AddPackageDefinition(definitionB);
            graph.AddPackageDefinition(definitionA);
            Queue<string> sortedQueue = graph.SortTopologically();
            Assert.AreEqual(2, sortedQueue.Count);
            Assert.AreEqual(definitionB.Package, sortedQueue.Dequeue());
            Assert.AreEqual(definitionA.Package, sortedQueue.Dequeue());
        }

        [Test]
        public void SortTopologically_returns_dependency_package_before_the_package_itself_when_multiple_dependencies_involved()
        {
            Graph graph = new Graph();
            PackageDefinition definitionA = new PackageDefinition("A:B,C,D");
            PackageDefinition definitionB = new PackageDefinition("B:C");
            PackageDefinition definitionC = new PackageDefinition("C");
            PackageDefinition definitionD = new PackageDefinition("D");
            graph.AddPackageDefinition(definitionA);
            graph.AddPackageDefinition(definitionC);
            graph.AddPackageDefinition(definitionB);
            graph.AddPackageDefinition(definitionD);
            Queue<string> sortedQueue = graph.SortTopologically();
            Assert.AreEqual(4, sortedQueue.Count);
            Assert.AreEqual(definitionC.Package, sortedQueue.Dequeue());
            Assert.AreEqual(definitionB.Package, sortedQueue.Dequeue());
            Assert.AreEqual(definitionD.Package, sortedQueue.Dequeue());
            Assert.AreEqual(definitionA.Package, sortedQueue.Dequeue());
        }

        [Test]
        public void SortTopologically_throws_exception_if_package_definitions_contain_cycle()
        {
            Graph graph = new Graph();
            PackageDefinition definitionA = new PackageDefinition("A:B");
            PackageDefinition definitionB = new PackageDefinition("B:A");
            graph.AddPackageDefinition(definitionA);
            graph.AddPackageDefinition(definitionB);
            Assert.Throws<InvalidOperationException>(() => graph.SortTopologically());
        }
    }
}
