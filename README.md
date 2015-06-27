# PackageInstaller
Package installer to topologically sort a list or packages with their dependencies

The installer builds a directed graph of packages where each vertex represents a package and each edge represents a dependency.
So if Package B depedens on A, The graph would be A -> B.
The graph is then sorted topologically to return a list of packages in order of installation.
This is implemented using TDD.
