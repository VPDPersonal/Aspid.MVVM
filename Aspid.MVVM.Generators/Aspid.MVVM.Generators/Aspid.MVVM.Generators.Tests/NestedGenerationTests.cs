#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.ViewModels;
using Aspid.MVVM.Generators.Generators.Ids;
using Xunit;

namespace MVVMGenerators.Tests;

public sealed class NestedGenerationTests
{
    private const string Model = "[Aspid.MVVM.ViewModel] public partial class Model { [Aspid.MVVM.Bind] private int _value; public int Read() => Value; public void Write(int value) => Value = value; }";

    [Theory]
    [InlineData("", "")]
    [InlineData("namespace Outer.Inner {", "}")]
    [InlineData("namespace Outer { namespace Inner {", "}}")]
    [InlineData("namespace Outer;", "")]
    [InlineData("namespace @event { namespace @class {", "}}")]
    [InlineData("public partial class Host {", "}")]
    [InlineData("public static partial class Host {", "}")]
    [InlineData("public partial struct Host {", "}")]
    [InlineData("public readonly partial struct Host {", "}")]
    [InlineData("public ref partial struct Host {", "}")]
    [InlineData("public partial record Host {", "}")]
    [InlineData("public partial record struct Host {", "}")]
    [InlineData("public partial interface Host {", "}")]
    [InlineData("public partial class Host<T> where T : class, new() {", "}")]
    public void GeneratedViewModelsCompileInTheirOriginalScope(string prefix, string suffix)
    {
        Compile(prefix + Model + suffix);
    }

    [Fact]
    public void GenericContainersKeepConstraintsAliasesAndPrivateAccessibilityAcrossFiles()
    {
        Compile(
            "using Parent = System.Exception; namespace Demo { public partial class Host<T> where T : Parent, new() { } }",
            "namespace Demo { public partial class Host<T> { private partial class Inner<U> where U : unmanaged { [Aspid.MVVM.ViewModel] private partial class Model<V> where V : class, new() { [Aspid.MVVM.Bind] private T _error; [Aspid.MVVM.Bind] private U _number; [Aspid.MVVM.Bind] private V _value; } } } }");
    }

    [Fact]
    public void EqualLeafNamesAndDifferentGenericAritiesDoNotCollide()
    {
        Compile("namespace Demo { public partial class A { " + Model + " } public partial class B { " + Model + " } public partial class A<T> { " + Model + " } }");
    }

    [Fact]
    public void NestedPrivateModelAssignsTheActualOriginalType()
    {
        var assembly = Compile("public partial class Host { " + Model.Replace("public partial class Model", "private partial class Model") + " public static object Create() => new Model(); }");
        var model = assembly.GetType("Host")!.GetMethod("Create")!.Invoke(null, null)!;
        model.GetType().GetMethod("Write")!.Invoke(model, new object[] { 7 });
        Assert.Equal(7, model.GetType().GetMethod("Read")!.Invoke(model, null));
    }

    [Theory]
    [InlineData("public class Host")]
    [InlineData("public struct Host")]
    [InlineData("public record Host")]
    public void NonPartialContainersReportADiagnosticInsteadOfFlatteningTheType(string container)
    {
        var result = Run(container + " { " + Model + " }").Result;
        var error = Assert.Single(result.Diagnostics.Where(d => d.Id == "ASPIDGEN001"));
        Assert.Equal("Host", error.Location.SourceTree!.GetText().ToString(error.Location.SourceSpan));
        Assert.DoesNotContain(result.Results.SelectMany(r => r.GeneratedSources), s => s.HintName.EndsWith("BindableMembers.g.cs"));
    }

    [Fact]
    public void EscapedTypeAndTypeParameterNamesCompile()
    {
        Compile("namespace @event { public partial class @class<@struct> where @struct : class { " +
            Model.Replace("class Model", "class @namespace").Replace("int _value", "@struct _value").Replace("public int Read()", "public @struct Read()").Replace("Write(int value)", "Write(@struct value)") + " } }");
    }

    internal static Assembly Compile(params string[] sources)
    {
        var (compilation, result) = Run(sources);
        Assert.DoesNotContain(result.Diagnostics, d => d.Severity == DiagnosticSeverity.Error);
        using var output = new MemoryStream();
        var emit = compilation.Emit(output);
        Assert.True(emit.Success, string.Join("\n", emit.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error)));
        return Assembly.Load(output.ToArray());
    }

    private static (Compilation Compilation, GeneratorDriverRunResult Result) Run(params string[] sources)
    {
        var refs = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        var compilation = CSharpCompilation.Create("Aspid.NestedTests." + Guid.NewGuid().ToString("N"),
            sources.Prepend(Framework).Select(source => CSharpSyntaxTree.ParseText(source)), refs,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new ViewModelGenerator(), new IdGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var output, out _);
        var result = driver.GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        return (output, result);
    }

    internal static readonly string Framework = ViewModelGeneratorTests.Framework
        .Replace("public readonly struct FindBindableMemberParameters { public void EnsureValid() { } }", "public readonly struct FindBindableMemberParameters { public string Id { get; } public void EnsureValid() { } }")
        .Replace("public readonly struct FindBindableMemberResult { }", "public readonly struct FindBindableMemberResult { public FindBindableMemberResult(object member) { } }") + """
        namespace Aspid.MVVM
        {
            public class TwoWayBindableMember<T> : IBindableMember<T>
            {
                public TwoWayBindableMember(T value, System.Action<T> setter) { Value = value; }
                public T Value { get; set; }
                public event System.Action<T> Changed;
                public BindMode Mode => BindMode.TwoWay;
                public IBinderRemover Add(IBinder binder) => null;
                public void Invoke(T value) { Value = value; Changed?.Invoke(value); }
            }
            public class TwoWayStructBindableMember<T> : TwoWayBindableMember<T>
            {
                public TwoWayStructBindableMember(T value, System.Action<T> setter) : base(value, setter) { }
            }
        }
        """;
}
