using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageInstaller
{
    public class Graph
    {
        private HashSet<PackageDefinition> Nodes;

        public Graph()
        {
            Nodes = new HashSet<PackageDefinition>();
        }

        public void AddPackageDefinition(PackageDefinition package)
        {
            Nodes.Add(package);
        }

        public bool Contains(string package)
        {
            foreach (PackageDefinition def in Nodes)
            {
                if (def.Package.Equals(package))
                    return true;
            }
            return false;
        }

        public Queue<string> SortTopologically()
        {
            Queue<string> result = new Queue<string>();
            foreach (PackageDefinition package in Nodes)
            {
                if (package.Added)
                    continue;

                BuildDependenciesFor(package,ref  result);
            }
            return result;
        }

        private void BuildDependenciesFor(PackageDefinition package, ref Queue<string> dependencyQueue)
        {
            foreach (string dependency in package.Dependencies)
            {
                PackageDefinition dependencyDefinition = Nodes.First(p => p.Package == dependency);
                if (dependencyDefinition.Added)
                    continue;
                if(dependencyDefinition.Visited)
                    throw new InvalidOperationException("Can't have a cycle in installation order");
                dependencyDefinition.MarkAsVisited();
                BuildDependenciesFor(dependencyDefinition,ref dependencyQueue);
            }
            dependencyQueue.Enqueue(package.Package);
            package.MarkAsAdded();
        }
    }
}
