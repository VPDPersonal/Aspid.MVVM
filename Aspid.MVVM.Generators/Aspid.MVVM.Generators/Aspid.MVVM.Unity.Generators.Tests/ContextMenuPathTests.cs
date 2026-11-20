using System.Linq;
using Aspid.MVVM.Unity.Generators.ContextMenu;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Aspid.MVVM.Unity.Generators.Tests;

public sealed class ContextMenuPathTests
{
    [Theory]
    [InlineData("Path = \"Explicit\", SubPath = \"Ignored\"", "CONTEXT/Target/Explicit")]
    [InlineData("SubPath = \"Numbers\"", "CONTEXT/Target/Add Target Binder/Numbers/Probe")]
    [InlineData("Path = \"\", SubPath = \"Numbers\"", "CONTEXT/Target/Add Target Binder/Numbers/Probe")]
    public void GeneratedMenusRespectExplicitPathPrecedence(string arguments, string expected)
    {
        var source = $$"""
            using System;
            namespace Aspid.MVVM
            {
                public class AddBinderContextMenuAttribute : Attribute
                {
                    public AddBinderContextMenuAttribute(Type type) { }
                    public string Path { get; set; }
                    public string SubPath { get; set; }
                }
            }
            namespace UnityEngine
            {
                public class AddComponentMenu : Attribute { public AddComponentMenu(string path) { } }
            }
            public class Target { }
            [UnityEngine.AddComponentMenu("Probe")]
            [Aspid.MVVM.AddBinderContextMenu(typeof(Target), {{arguments}})]
            public class Probe { }
            """;
        var compilation = CSharpCompilation.Create("MenuProbe",
            new[] { CSharpSyntaxTree.ParseText(source) },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var result = CSharpGeneratorDriver.Create(new AddBinderContextMenuGenerator())
            .RunGenerators(compilation).GetRunResult();
        Assert.All(result.Results, item => Assert.Null(item.Exception));
        Assert.Contains(expected, string.Concat(result.GeneratedTrees.Select(tree => tree.ToString())));
    }
}
