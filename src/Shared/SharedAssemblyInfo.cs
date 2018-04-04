using System.Reflection;

[assembly: AssemblyProduct("Core.CodeGeneration")]

[assembly: AssemblyCompany("CustomCode")]
[assembly: AssemblyCopyright("Copyright © 2018")]
[assembly: AssemblyTrademark("C# code generation via *.csx scripts")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif