using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Unity.Generators.ContextMenu;
using Xunit;

namespace Aspid.MVVM.Unity.Generators.Tests;

public sealed class NestedContextMenuTests
{
    private const string Target = "[Aspid.MVVM.AddBinderContextMenu(typeof(UnityEngine.Component))] public class Probe : UnityEngine.Component { }";

    [Theory]
    [InlineData("namespace Outer { namespace Inner {", "}}")]
    [InlineData("namespace Outer.Inner;", "")]
    [InlineData("public class Host {", "}")]
    [InlineData("public class A {", "} public class B { [Aspid.MVVM.AddBinderContextMenu(typeof(UnityEngine.Component))] public class Probe : UnityEngine.Component {} }")]
    [InlineData("namespace @event { public class @class {", "}}")]
    [InlineData("", "")]
    public void AccessibleTargetsGenerateUniqueCompilableMenus(string prefix, string suffix)
    {
        var (compilation, result) = Run(prefix + Target + suffix);
        Assert.DoesNotContain(result.Diagnostics, d => d.Severity == DiagnosticSeverity.Error);
        using var stream = new MemoryStream();
        var emit = compilation.Emit(stream);
        Assert.True(emit.Success, string.Join("\n", emit.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error)));
        Assert.NotEmpty(result.GeneratedTrees);
    }

    [Theory]
    [InlineData("public class Host<T> {", "}")]
    [InlineData("public class Host { private class Inner {", "}}")]
    [InlineData("public class Host { protected class Inner {", "}}")]
    public void OpenOrInaccessibleTargetsProduceAnActionableDiagnostic(string prefix, string suffix)
    {
        var (_, result) = Run(prefix + Target + suffix);
        Assert.Single(result.Diagnostics.Where(d => d.Id == "ASPIDMENU001"));
        Assert.Empty(result.GeneratedTrees);
    }

    private static (Compilation Compilation, GeneratorDriverRunResult Result) Run(string source)
    {
        var options = new CSharpParseOptions(LanguageVersion.Preview, preprocessorSymbols: new[] { "UNITY_EDITOR" });
        var refs = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        var compilation = CSharpCompilation.Create("Menus", new[] { CSharpSyntaxTree.ParseText(Framework, options), CSharpSyntaxTree.ParseText(source, options) },
            refs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new[] { new AddBinderContextMenuGenerator().AsSourceGenerator() }, parseOptions: options);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var output, out _);
        var result = driver.GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        return (output, result);
    }

    private const string Framework = """
        namespace Aspid.MVVM { public class AddBinderContextMenuAttribute : System.Attribute { public AddBinderContextMenuAttribute(System.Type type) {} } }
        namespace UnityEngine { public class GameObject { public void AddComponent(System.Type type) {} } public class Component { public GameObject gameObject; } }
        namespace UnityEditor { public class MenuItem : System.Attribute { public int priority; public MenuItem(string path) {} } public class MenuCommand { public object context; } }
        """;
}
