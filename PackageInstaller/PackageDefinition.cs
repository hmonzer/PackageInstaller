using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageInstaller
{
    /// <summary>
    /// Entity used to consolidate logic for parsing input as a string A: B, C, D and map it to a Package with a list of Dependencies
    /// </summary>
    public class PackageDefinition
    {
        public string Package { get; set; }

        private List<string> _dependencies { get; set; }

        public IEnumerable<string> Dependencies
        {
            get { return _dependencies.AsEnumerable(); }
        }

        public PackageDefinition(string packageDefinition)
        {
            
        }
    }
}
