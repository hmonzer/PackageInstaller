# PackageInstaller
Package installer Takes a list of packages with their dependencies and outputs a list of packages in the order of install.

The installer builds a directed graph of packages where each vertex represents a package and each edge represents a dependency.
So if Package A depedens on B, The graph would be A -> B.
The graph is then sorted topologically to return a list of packages in order of installation.
This is implemented using TDD.
