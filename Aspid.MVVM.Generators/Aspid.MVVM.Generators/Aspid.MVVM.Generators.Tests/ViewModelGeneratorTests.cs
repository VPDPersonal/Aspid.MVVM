using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.ViewModels;
using Xunit;

namespace MVVMGenerators.Tests;

/// <summary>
/// Tests for the ViewModel generator's implementation of custom ViewModel interfaces: an interface that exposes
/// bindable members as properties gets them backed by the members the generator emits.
/// </summary>
public sealed class ViewModelGeneratorTests
{
    private const string Framework = """
        using System;

        namespace Aspid.MVVM
        {
            public enum BindMode { None, OneWay, TwoWay, OneTime, OneWayToSource }

            public interface IBinder { }
            public interface IBinderRemover { }
            public interface IBinderAdder { BindMode Mode { get; } IBinderRemover Add(IBinder binder); }
            public interface IReadOnlyValueBindableMember<out T> : IBinderAdder { T Value { get; } }
            public interface IReadOnlyBindableMember<out T> : IReadOnlyValueBindableMember<T> { event Action<T> Changed; }
            public interface IBindableMember<T> : IReadOnlyBindableMember<T> { new T Value { set; } }

            public readonly struct FindBindableMemberParameters { }
            public readonly struct FindBindableMemberResult { }

            public interface IViewModel { FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters); }

            [AttributeUsage(AttributeTargets.Class)]
            public sealed class ViewModelAttribute : Attribute { }

            [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
            public class BindAttribute : Attribute { public BindAttribute() { } public BindAttribute(BindMode mode) { } }
        }
        """;

    /// <summary>
    /// An interface slot typed <c>IBindableMember</c> is a custom ViewModel interface property, and a two-way
    /// member backs it.
    /// </summary>
    [Fact]
    public void ATwoWaySlotIsImplementedByATwoWayMember()
    {
        var generated = Run("""
            using Aspid.MVVM;

            public interface IPlayerViewModel : IViewModel
            {
                IBindableMember<int> Health { get; }
            }

            [ViewModel]
            public partial class PlayerViewModel : IPlayerViewModel
            {
                [Bind(BindMode.TwoWay)] private int _health;
            }
            """);

        Assert.Contains("IPlayerViewModel.Health =>", generated);
    }

    /// <summary>
    /// A read-only generated member cannot back a two-way slot, so the explicit implementation is not emitted
    /// (it would not compile).
    /// </summary>
    [Fact]
    public void ATwoWaySlotIsNotImplementedByAReadOnlyMember()
    {
        var generated = Run("""
            using Aspid.MVVM;

            public interface IPlayerViewModel : IViewModel
            {
                IBindableMember<int> Health { get; }
            }

            [ViewModel]
            public partial class PlayerViewModel : IPlayerViewModel
            {
                [Bind(BindMode.OneWay)] private int _health;
            }
            """);

        Assert.DoesNotContain("IPlayerViewModel.Health", generated);
    }

    /// <summary>A read-only slot accepts a read-only member as well as a two-way one.</summary>
    [Fact]
    public void AReadOnlySlotAcceptsAReadOnlyAndATwoWayMember()
    {
        var generated = Run("""
            using Aspid.MVVM;

            public interface IPlayerViewModel : IViewModel
            {
                IReadOnlyBindableMember<int> Health { get; }
                IReadOnlyBindableMember<int> Mana { get; }
            }

            [ViewModel]
            public partial class PlayerViewModel : IPlayerViewModel
            {
                [Bind(BindMode.OneWay)] private int _health;
                [Bind(BindMode.TwoWay)] private int _mana;
            }
            """);

        Assert.Contains("IPlayerViewModel.Health =>", generated);
        Assert.Contains("IPlayerViewModel.Mana =>", generated);
    }

    /// <summary>
    /// The marker of <c>FindBindableMember</c> compiles only under <c>ENABLE_PROFILER</c>, so a release player
    /// carries no marker field.
    /// </summary>
    [Fact]
    public void TheProfilerMarkerCompilesOnlyUnderEnableProfiler()
    {
        var generated = Run("""
            using Aspid.MVVM;

            [ViewModel]
            public partial class PlayerViewModel
            {
                [Bind] private int _health;
            }
            """);

        Assert.Contains("__findBindableMemberMarker", generated);
        Assert.Equal(2, CountOf(generated, "#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED"));
        Assert.DoesNotContain("#if !ASPID_MVVM_UNITY_PROFILER_DISABLED", generated);
    }

    private static int CountOf(string text, string value) =>
        Regex.Matches(text, Regex.Escape(value)).Count;

    private static string Run(string declaration)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "Probe.ViewModels",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(Framework),
                CSharpSyntaxTree.ParseText(declaration),
            },
            references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return string.Concat(CSharpGeneratorDriver
            .Create(new ViewModelGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .GeneratedTrees
            .Select(tree => tree.ToString()));
    }
}
