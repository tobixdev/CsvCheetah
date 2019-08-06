using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class MapCompiler<T> : IMapCompiler<T>
    {
        private const string tokenStreamName = "tokenStream";
        private readonly CodeCompileUnit _codeCompileUnit;

        private CodeMemberMethod _mappingMethod;

        public MapCompiler()
        {
            _codeCompileUnit = new CodeCompileUnit();
        }

        public ITokenStreamMapper<T> CompileMap(IMap<T> map)
        {
            PrepareCompilation();

            _mappingMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(null)));

            var generatedAssembly = CompileToAssembly();

            return (ITokenStreamMapper<T>) generatedAssembly.CreateInstance(
                "tobixdev.github.io.CsvCheetah.Mapping.MyClass");
        }

        private Assembly CompileToAssembly()
        {
            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                IncludeDebugInformation = false,
                GenerateInMemory = true
            };

            var compilerResult = new CSharpCodeProvider().CompileAssemblyFromDom(parameters, _codeCompileUnit);

            return compilerResult.CompiledAssembly;
        }

        private void PrepareCompilation()
        {
            var targetNamespace = CreateNamespace();
            var targetClass = CreateClass();
            _mappingMethod = CreateMappingMethod();

            CodeNamespace CreateNamespace()
            {
                var result = new CodeNamespace("tobixdev.github.io.CsvCheetah.Mapping");
                result.Imports.Add(new CodeNamespaceImport("System"));
                _codeCompileUnit.Namespaces.Add(result);
                return result;
            }

            CodeTypeDeclaration CreateClass()
            {
                var result = new CodeTypeDeclaration("MyClass");
                result.BaseTypes.Add(typeof(ITokenStreamMapper<T>));
                targetNamespace.Types.Add(result);
                return result;
            }

            CodeMemberMethod CreateMappingMethod()
            {
                var result = new CodeMemberMethod
                {
                    Name = "Map",
                    ReturnType = new CodeTypeReference(typeof(IEnumerable<T>))
                };

                result.Parameters.Add(
                    new CodeParameterDeclarationExpression(typeof(IEnumerable<Token>), tokenStreamName));

                targetClass.Members.Add(result);

                return result;
            }
        }
    }
}