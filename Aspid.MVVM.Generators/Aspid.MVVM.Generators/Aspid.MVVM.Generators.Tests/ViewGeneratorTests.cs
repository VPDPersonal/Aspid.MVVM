using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.Views;
using Xunit;

namespace MVVMGenerators.Tests;

/// <summary>
/// Tests for the View generator's binder fields: the fields it emits for the bindable members of the ViewModel
/// named by <c>IView&lt;TViewModel&gt;</c>, and the members it already recognises as binders.
/// </summary>
public sealed class ViewGeneratorTests
{
    private const string Framework = """
        using System;

        namespace UnityEngine
        {
            public class Object { }
            public class Component : Object { }
            public class MonoBehaviour : Component { }
            public class SerializeField : Attribute { }
        }

        namespace Aspid.MVVM
        {
            public enum BindMode { None, OneWay, TwoWay, OneTime, OneWayToSource }

            public interface IBinder { }
            public interface IViewModel { }

            public interface IView { IViewModel ViewModel { get; } void Initialize(IViewModel viewModel); void Deinitialize(); }
            public interface IView<in T> : IView where T : IViewModel { void Initialize(T viewModel); }

            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
            public sealed class ViewAttribute : Attribute { public bool AutoBinderFields { get; set; } = true; }

            [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
            public class BindAttribute : Attribute { public BindAttribute() { } public BindAttribute(BindMode mode) { } }

            [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
            public sealed class IgnoreBindAttribute : Attribute { }
        }
        """;

    /// <summary>
    /// A derived View whose base View already gets a binder field generated from <c>IView&lt;TViewModel&gt;</c> does
    /// not emit that field again — Unity would see the same serialized name twice.
    /// </summary>
    [Fact]
    public void ADerivedViewDoesNotRepeatTheGeneratedFieldOfItsBase()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;

            public class PlayerViewModel : IViewModel
            {
                [Bind] private int _health;
            }

            [View]
            public partial class BaseView : MonoBehaviour, IView<PlayerViewModel> { }

            [View]
            public partial class DerivedView : BaseView, IView<PlayerViewModel> { }
            """);

        Assert.Single(Regex.Matches(generated, @"MonoBinder\[\] _health;"));
        Assert.Contains("partial class BaseView", generated);
        Assert.Contains("partial class DerivedView", generated);
    }

    /// <summary>
    /// A generic base View names no ViewModel in its own partial, so the View that closes it with a ViewModel type
    /// still emits the binder fields for that ViewModel.
    /// </summary>
    [Fact]
    public void AViewClosingAGenericBaseKeepsTheGeneratedField()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;

            public class PlayerViewModel : IViewModel
            {
                [Bind] private int _health;
            }

            [View]
            public partial class BaseView<T> : MonoBehaviour, IView<T> where T : IViewModel { }

            [View]
            public partial class DerivedView : BaseView<PlayerViewModel> { }
            """);

        Assert.Single(Regex.Matches(generated, @"MonoBinder\[\] _health;"));
        Assert.Contains("partial class DerivedView", generated);
    }

    /// <summary>
    /// A field typed as the bare <c>IBinder</c> interface is a binder: it is bound and unbound with the View,
    /// like a field of a concrete binder type.
    /// </summary>
    [Fact]
    public void AFieldTypedAsTheBareBinderInterfaceIsABinder()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;

            [View]
            public partial class PlayerView : MonoBehaviour, IView<IViewModel>
            {
                [SerializeField] private IBinder _score;
            }
            """);

        Assert.Contains("_score.BindSafely(", generated);
        Assert.Contains("_score.UnbindSafely(", generated);
    }

    /// <summary>
    /// The nullable annotation does not hide the bare <c>IBinder</c> interface: an <c>IBinder?</c> field under
    /// <c>#nullable enable</c> is a binder too.
    /// </summary>
    [Fact]
    public void ANullableFieldTypedAsTheBareBinderInterfaceIsABinder()
    {
        var generated = Run("""
            #nullable enable
            using Aspid.MVVM;
            using UnityEngine;

            [View]
            public partial class PlayerView : MonoBehaviour, IView<IViewModel>
            {
                [SerializeField] private IBinder? _score;
            }
            """);

        Assert.Contains("_score.BindSafely(", generated);
        Assert.Contains("_score.UnbindSafely(", generated);
    }

    /// <summary>A binder field or property marked <c>[IgnoreBind]</c> is left out of the generated binding.</summary>
    [Fact]
    public void AnIgnoredBinderMemberIsNotBound()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;

            [View]
            public partial class PlayerView : MonoBehaviour, IView<IViewModel>
            {
                [SerializeField] private IBinder _score;
                [SerializeField, IgnoreBind] private IBinder _ignoredField;
                [IgnoreBind] private IBinder IgnoredProperty => _score;
            }
            """);

        Assert.Contains("_score.BindSafely(", generated);
        Assert.DoesNotContain("_ignoredField", generated);
        Assert.DoesNotContain("IgnoredProperty", generated);
    }

    /// <summary>
    /// A member marked <c>[IgnoreBind]</c> is bound by hand but still declared: no auto binder field is emitted for
    /// the ViewModel member of the same name, in the View itself or in a View derived from it.
    /// </summary>
    [Fact]
    public void AnIgnoredBinderMemberStillBlocksTheAutoBinderField()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;

            public class PlayerViewModel : IViewModel
            {
                [Bind] private int _health;
                [Bind] private int _mana;
            }

            [View]
            public partial class PlayerView : MonoBehaviour, IView<PlayerViewModel>
            {
                [SerializeField, IgnoreBind] private IBinder _health;
            }

            [View]
            public partial class BaseView : MonoBehaviour
            {
                [SerializeField, IgnoreBind] private IBinder _health;
            }

            [View]
            public partial class DerivedView : BaseView, IView<PlayerViewModel> { }
            """);

        Assert.DoesNotContain("MonoBinder[] _health;", generated);
        Assert.Equal(2, CountOf(generated, "MonoBinder[] _mana;"));
        Assert.DoesNotContain("_health.BindSafely(", generated);
    }

    /// <summary>
    /// The <c>Initialize</c> and <c>Deinitialize</c> markers compile only under <c>ENABLE_PROFILER</c>, so a release
    /// player carries no marker field.
    /// </summary>
    [Fact]
    public void TheProfilerMarkersCompileOnlyUnderEnableProfiler()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;

            public class PlayerViewModel : IViewModel
            {
                [Bind] private int _health;
            }

            [View]
            public partial class PlayerView : MonoBehaviour, IView<PlayerViewModel> { }
            """);

        Assert.Contains("__initializeMarker", generated);
        Assert.Contains("__deinitializeMarker", generated);
        Assert.Contains("#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED", generated);
        Assert.DoesNotContain("#if !ASPID_MVVM_UNITY_PROFILER_DISABLED", generated);
    }

    private static int CountOf(string text, string value) =>
        Regex.Matches(text, Regex.Escape(value)).Count;

    private static string Run(string declaration)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "Probe.Views",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(Framework),
                CSharpSyntaxTree.ParseText(declaration),
            },
            references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return string.Concat(CSharpGeneratorDriver
            .Create(new ViewGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .GeneratedTrees
            .Select(tree => tree.ToString()));
    }
}
