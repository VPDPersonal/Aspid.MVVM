#nullable enable

using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.Views;
using Aspid.MVVM.Generators.Generators.Binders;
using Aspid.MVVM.Generators.Generators.SerializableBinders;
using Xunit;

namespace MVVMGenerators.Tests;

public sealed class NestedAdditionalGenerationTests
{
    private const string View = "[Aspid.MVVM.View(AutoBinderFields = false)] public partial class Panel : Aspid.MVVM.IView<Model> { } public class Model : Aspid.MVVM.IViewModel { }";
    private const string Binder = "public partial class Probe : Aspid.MVVM.IBinder<int> { [Aspid.MVVM.BinderLog] public void SetValue(int value) { } }";
    private const string Mono = "[Aspid.MVVM.StarterKit.GenerateSerializableBinder] public class ProbeMonoBinder : Aspid.MVVM.StarterKit.ComponentFloatMonoBinder<UnityEngine.Camera> { }";

    [Theory]
    [InlineData("", "")]
    [InlineData("namespace Outer { namespace Inner {", "}}")]
    [InlineData("namespace Outer.Inner;", "")]
    [InlineData("public partial class Host {", "}")]
    [InlineData("public partial class Host<T> where T : System.Exception, new() {", "}")]
    [InlineData("namespace @event { public partial class @class<@struct> where @struct : class {", "}}")]
    public void ViewsCompileIncludingProfilerForNestedGenericTypes(string prefix, string suffix)
    {
        Compile(ViewFramework, new ViewGenerator(), prefix + View + suffix);
    }

    [Fact]
    public void ViewModelNamedInitializeDoesNotCollideWithTheViewInfrastructure()
    {
        Compile(ViewFramework, new ViewGenerator(), View.Replace("IView<Model>", "IView<Initialize>").Replace("class Model", "class Initialize"));
    }

    [Fact]
    public void SameNamedViewsInDifferentContainersDoNotCollide()
    {
        Compile(ViewFramework, new ViewGenerator(), "partial class A { " + View + " } partial class B { " + View + " }");
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("namespace Outer { namespace Inner {", "}}")]
    [InlineData("public partial class Host<T> where T : class {", "}")]
    public void BinderLogCompilesInOriginalScope(string prefix, string suffix)
    {
        Compile(BinderFramework, new BinderGenerator(), prefix + Binder + suffix);
    }

    [Fact]
    public void SameNamedBindersAndPartialDeclarationsDoNotCollide()
    {
        Compile(BinderFramework, new BinderGenerator(), "partial class A { " + Binder + " } partial class B { " + Binder + " }",
            "partial class A { public partial class Probe : Aspid.MVVM.IBinder<int> { } }");
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("namespace Outer { namespace Inner {", "}}")]
    [InlineData("namespace Outer.Inner;", "")]
    [InlineData("public partial class Host {", "}")]
    [InlineData("public partial class Host<T> where T : class, new() {", "}")]
    [InlineData("namespace @event { public partial class @class<@struct> where @struct : class {", "}}")]
    public void SerializableBindersCompileInOriginalScope(string prefix, string suffix)
    {
        Compile(SerializableFramework, new SerializableBinderGenerator(),
            "using Aspid.MVVM; using Aspid.MVVM.StarterKit; " + prefix + Mono + suffix);
    }

    [Fact]
    public void SerializableBindersPreserveNamespaceScopedAliasesAndGenericConstraints()
    {
        Compile(SerializableFramework, new SerializableBinderGenerator(), """
            namespace Outer
            {
                using Aspid.MVVM;
                using Aspid.MVVM.StarterKit;
                using CameraAlias = UnityEngine.Camera;
                namespace Inner
                {
                    [GenerateSerializableBinder]
                    public class ProbeMonoBinder<T> : ComponentFloatMonoBinder<CameraAlias> where T : class, new()
                    {
                        public T Create() => new T();
                    }
                }
            }
            """);
    }

    [Fact]
    public void PrivateSerializableTwinsKeepConstraintsOnPrivateTypes()
    {
        Compile(SerializableFramework, new SerializableBinderGenerator(), """
            using Aspid.MVVM;
            using Aspid.MVVM.StarterKit;
            partial class Host
            {
                private class Payload { }
                [GenerateSerializableBinder]
                private class ProbeMonoBinder<T> : ComponentFloatMonoBinder<UnityEngine.Camera> where T : Payload { }
            }
            """);
    }

    [Fact]
    public void SerializableBinderHintNamesIncludeNamespaceContainerAndArity()
    {
        Compile(SerializableFramework, new SerializableBinderGenerator(),
            "using Aspid.MVVM; using Aspid.MVVM.StarterKit; namespace A { " + Mono + Mono.Replace("class ProbeMonoBinder", "class ProbeMonoBinder<T>") + " } namespace B { " + Mono + " }");
    }

    [Fact]
    public void ExistingNestedTwinPreventsGenerationInTheSameScope()
    {
        var (_, result) = Run(SerializableFramework, new SerializableBinderGenerator(),
            "partial class Host { " + Mono + " public class ProbeBinder { } }");
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDSB003");
        Assert.Empty(result.Results.SelectMany(generator => generator.GeneratedSources));
    }

    [Fact]
    public void EscapedSerializableNameRecognizesAnExistingTwin()
    {
        var (_, result) = Run(SerializableFramework, new SerializableBinderGenerator(),
            "partial class Host { " + Mono.Replace("class ProbeMonoBinder", "class @ProbeMonoBinder") + " public class ProbeBinder { } }");
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDSB003");
        Assert.Empty(result.Results.SelectMany(generator => generator.GeneratedSources));
    }

    [Theory]
    [InlineData("view")]
    [InlineData("binder")]
    [InlineData("serializable")]
    public void EveryPartialGeneratorDiagnosesNonPartialContainers(string kind)
    {
        var generator = kind == "view" ? (IIncrementalGenerator)new ViewGenerator()
            : kind == "binder" ? new BinderGenerator() : new SerializableBinderGenerator();
        var framework = kind == "view" ? ViewFramework : kind == "binder" ? BinderFramework : SerializableFramework;
        var declaration = kind == "view" ? View : kind == "binder" ? Binder : Mono;
        var (_, result) = Run(framework, generator, "class Host { " + declaration + " }");
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDGEN001");
        Assert.Empty(result.Results.SelectMany(item => item.GeneratedSources));
    }

    private static void Compile(string framework, IIncrementalGenerator generator, params string[] sources)
    {
        var (compilation, result) = Run(framework, generator, sources);
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        using var output = new MemoryStream();
        var emit = compilation.Emit(output);
        Assert.True(emit.Success, string.Join("\n", emit.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)));
    }

    private static (Compilation Compilation, GeneratorDriverRunResult Result) Run(string framework, IIncrementalGenerator generator, params string[] sources)
    {
        var options = CSharpParseOptions.Default.WithPreprocessorSymbols("UNITY_EDITOR", "ASPID_MVVM_PROFILER");
        var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        var compilation = CSharpCompilation.Create("Aspid.NestedAdditionalTests",
            sources.Prepend(framework).Select(source => CSharpSyntaxTree.ParseText(source, options)), references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new[] { generator.AsSourceGenerator() }, parseOptions: options);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var output, out _);
        var result = driver.GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        return (output, result);
    }

    private const string ProfilerFramework = """
        namespace Unity.Profiling
        {
            public struct ProfilerMarker
            {
                public ProfilerMarker(string name) { }
                public Scope Auto() => new Scope();
                public struct Scope : System.IDisposable { public void Dispose() { } }
            }
        }
        """;

    private static readonly string ViewFramework = ViewGeneratorTests.Framework + ProfilerFramework + """
        namespace Aspid.MVVM
        {
            public readonly struct UnbindContext { public UnbindContext(object owner) { } }
            public static class BinderExtensions { }
        }
        """;

    private const string BinderFramework = ProfilerFramework + """
        namespace UnityEngine { public class SerializeField : System.Attribute { } }
        namespace Aspid.MVVM
        {
            public interface IBinder { }
            public interface IBinder<T> : IBinder { void SetValue(T value); }
            public class BinderLogAttribute : System.Attribute { }
        }
        """;

    private static readonly string SerializableFramework = SerializableBinderGeneratorTests.Framework
        .Replace("protected T Target;\n        protected TargetFloatBinder", "protected T Target;\n        protected TargetFloatBinder() { }\n        protected TargetFloatBinder") + """
        namespace Aspid.MVVM
        {
            public static class BindModeExtensions { public static void EnsureNot(this BindMode mode, BindMode forbidden) { } }
        }
        """;
}
