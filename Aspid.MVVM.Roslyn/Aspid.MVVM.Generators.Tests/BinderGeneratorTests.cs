using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.Binders;
using Xunit;

namespace MVVMGenerators.Tests;

/// <summary>
/// Tests for the binder generator's <c>[BinderLog]</c> partial.
/// </summary>
public sealed class BinderGeneratorTests
{
    private const string Framework = """
        using System;

        namespace Aspid.MVVM
        {
            public interface IBinder { }
            public interface IBinder<in T> : IBinder { void SetValue(T value); }
            public interface IAnyBinder : IBinder { void SetValue<T>(T value); }

            [AttributeUsage(AttributeTargets.Method)]
            public sealed class BinderLogAttribute : Attribute { }
        }
        """;

    /// <summary>
    /// The <c>SetValue</c> marker and both of its uses compile only under <c>ENABLE_PROFILER</c>, and
    /// <c>ASPID_MVVM_UNITY_PROFILER_DISABLED</c> turns them off.
    /// </summary>
    [Fact]
    public void TheSetValueMarkerCompilesOnlyUnderEnableProfiler()
    {
        var generated = Run("""
            using Aspid.MVVM;

            public partial class TextBinder : IBinder<string>
            {
                [BinderLog]
                public void SetValue(string value) { }
            }
            """);

        Assert.Equal(2, CountOf(generated, "using (SetValueMarker.Auto())"));
        Assert.Equal(3, CountOf(generated, "#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED"));
    }

    private static int CountOf(string text, string value) =>
        Regex.Matches(text, Regex.Escape(value)).Count;

    private static string Run(string declaration)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "Probe.Binders",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(Framework),
                CSharpSyntaxTree.ParseText(declaration),
            },
            references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return string.Concat(CSharpGeneratorDriver
            .Create(new BinderGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .GeneratedTrees
            .Select(tree => tree.ToString()));
    }
}
