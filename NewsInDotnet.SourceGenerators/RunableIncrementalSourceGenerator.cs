using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace NewsInDotnet.SourceGenerators
{
    /// <summary>
    /// 使用SourceGenerator绕过Reflection，符合NativeAOT
    /// </summary>
    [Generator]
    public class RunableIncrementalSourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            //Debugger.Launch();
            // 获取所有的类声明语法节点
            IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations = context.SyntaxProvider
               .CreateSyntaxProvider(
                    predicate: (syntaxNode, _) => syntaxNode is ClassDeclarationSyntax,
                    transform: (generatorSyntaxContext, _) => (ClassDeclarationSyntax)generatorSyntaxContext.Node)
               .Where(classDeclaration => classDeclaration != null);

            // 获取编译对象
            IncrementalValueProvider<Compilation> compilationProvider = context.CompilationProvider;

            // 组合编译对象和类声明
            IncrementalValueProvider<(Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right)> compilationAndClasses =
                compilationProvider.Combine(classDeclarations.Collect());

            // 生成代码
            context.RegisterSourceOutput(compilationAndClasses, (sourceProductionContext, tuple) =>
            {
                var (compilation, classDeclarations) = tuple;
                // 获取 IRunable 接口的类型符号
                var runableInterface = compilation.GetTypeByMetadataName("NewsInCSharp.IRunable");
                if (runableInterface == null)
                {
                    return;
                }

                var runableClasses = new List<string>();
                foreach (var classDeclaration in classDeclarations)
                {
                    var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                    var classSymbol = model.GetDeclaredSymbol(classDeclaration);
                    if (classSymbol != null && classSymbol.AllInterfaces.Contains(runableInterface))
                    {
                        runableClasses.Add(classSymbol.ToDisplayString());
                    }
                }

                // 生成代码
                var source = $@"
using System;
using System.Reflection;
using System.Collections.Generic;

namespace NewsInCSharp
{{
    public static class RunableClassCollector
    {{
        public static readonly List<NewsInCSharp.IRunable> RunableInstances = new List<NewsInCSharp.IRunable>
        {{
            {string.Join(",\n            ", runableClasses.Select(c => $"new {c}()"))}
        }};
    }}
}}";

                // 添加生成的代码到编译过程中
                sourceProductionContext.AddSource("RunableClassCollector.g.cs", SourceText.From(source, System.Text.Encoding.UTF8));
            });
        }
    }
}
