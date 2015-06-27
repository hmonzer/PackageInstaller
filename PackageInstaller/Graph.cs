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
                if (result.Contains(package.Package))
                    continue;
                result = BuildDependenciesFor(package);
            }
            return result;
        }

        private Queue<string> BuildDependenciesFor(PackageDefinition package)
        {
            Queue<string> result = new Queue<string>();
            foreach (string dependency in package.Dependencies)
            {
                if (result.Contains(dependency))
                    continue;
                Queue<string> dependencyQueue = BuildDependenciesFor(Nodes.First(p => p.Package == dependency));
               while(dependencyQueue.Any())
                    result.Enqueue(dependencyQueue.Dequeue());
            }
            result.Enqueue(package.Package);
            return result;
        }
    }
}
