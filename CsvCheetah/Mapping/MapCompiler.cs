using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class MapCompiler<T> : IMapCompiler<T>
    {

        public ITokenStreamMapper<T> CompileMap(IMap<T> map)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(@"
    using System;

    namespace RoslynCompileSample
    {
        public class Writer
        {
            public void Write(string message)
            {
                Console.WriteLine(message);
            }
        }
    }");
            
            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.TargetedPatchingOptOutAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                
                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                    throw new Exception();
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());
                    var x = assembly.CreateInstance("RoslynCompileSample.Writer");
                    if(x == null)
                        throw new Exception("x");
                    return null;
                }
            }
        }
    }
}