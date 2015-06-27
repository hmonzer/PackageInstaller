using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageInstaller
{
    public class PackageDependencyResolver
    {
        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                throw new Exception("No packages provided for installation");
            }
            Graph graph = new Graph();
            foreach (string arg in args)
            {
                graph.AddPackageDefinition(new PackageDefinition(arg));
            }
            Queue<string> sortedInstallationOrder = graph.SortTopologically();
            Console.Write(sortedInstallationOrder.Dequeue());
            while(sortedInstallationOrder.Any()) 
                Console.Write(", " + sortedInstallationOrder.Dequeue());
        }
    }
}
