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
    }
}
