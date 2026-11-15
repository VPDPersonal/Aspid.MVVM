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
    internal const string Framework = """
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
            public sealed class AsBinderAttribute : Attribute
            {
                public AsBinderAttribute(Type type, params object[] arguments) { }
            }

            [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
            public sealed class IgnoreBindAttribute : Attribute { }
        }
        """;

    [Fact]
    public void TypedViewsUseBindContextsForDeclaredAndGeneratedBinderFields()
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
                [SerializeField] private IBinder _health;
            }
            """);

        Assert.Contains("AspidBinderExtensions.BindSafely(_health, new global::Aspid.MVVM.BindContext(", generated);
        Assert.Contains("AspidBinderExtensions.BindSafely(_mana, new global::Aspid.MVVM.BindContext(", generated);
    }

    [Theory]
    [InlineData("private IBinder[] _scores;")]
    [InlineData("private IBinder[] Scores => new IBinder[0];")]
    public void BinderArraysUseCollectionUnbind(string member)
    {
        var generated = Run($$"""
            using Aspid.MVVM;
            using UnityEngine;

            [View]
            public partial class PlayerView : MonoBehaviour, IView<IViewModel>
            {
                {{member}}
            }
            """);

        Assert.Contains("AspidBinderExtensions.Unbind(", generated);
        Assert.DoesNotContain("((global::Aspid.MVVM.IBinder)", generated);
    }

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

        Assert.Contains("AspidBinderExtensions.BindSafely(_score, ", generated);
        Assert.Contains("((global::Aspid.MVVM.IBinder)_score)?.Unbind(in __context);", generated);
        Assert.DoesNotContain("AspidBinderExtensions.Unbind(_score,", generated);
        Assert.Contains("using AspidBinderExtensions = global::Aspid.MVVM.BinderExtensions;", generated);
        Assert.DoesNotContain("ViewBindings", generated);
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

        Assert.Contains("AspidBinderExtensions.BindSafely(_score, ", generated);
        Assert.Contains("((global::Aspid.MVVM.IBinder)_score)?.Unbind(in __context);", generated);
        Assert.DoesNotContain("AspidBinderExtensions.Unbind(_score,", generated);
        Assert.Contains("using AspidBinderExtensions = global::Aspid.MVVM.BinderExtensions;", generated);
        Assert.DoesNotContain("ViewBindings", generated);
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

        Assert.Contains("AspidBinderExtensions.BindSafely(_score, ", generated);
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
        Assert.DoesNotContain("AspidBinderExtensions.BindSafely(_health, ", generated);
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

    [Fact]
    public void SingleHeaderGroupsDoNotLeakIntoFollowingGeneratedFields()
    {
        var generated = Run("""
            using Aspid.MVVM;
            using UnityEngine;
            namespace Aspid.MVVM
            {
                public sealed class HeaderGroupAttribute : System.Attribute { public HeaderGroupAttribute(string title) { } }
                public sealed class HeaderGroupStartAttribute : System.Attribute { public HeaderGroupStartAttribute(string title) { } }
                public sealed class HeaderGroupEndAttribute : System.Attribute { }
            }
            public class Model : IViewModel
            {
                [HeaderGroupStart("A"), Bind] private int _first;
                [HeaderGroup("B"), Bind] private int _single;
                [Bind] private int _after;
                [HeaderGroupEnd, Bind] private int _end;
                [Bind] private int _last;
            }
            [View] public partial class Probe : MonoBehaviour, IView<Model> { }
            """);

        Assert.Matches("HeaderGroupAttribute\\(\"A\"\\)[^;]*_first;", generated);
        Assert.Matches("HeaderGroupAttribute\\(\"B\"\\)[^;]*_single;", generated);
        Assert.Matches("HeaderGroupAttribute\\(\"A\"\\)[^;]*_after;", generated);
        Assert.DoesNotMatch("HeaderGroupAttribute[^;]*_end;", generated);
        Assert.DoesNotMatch("HeaderGroupAttribute[^;]*_last;", generated);
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

        var result = CSharpGeneratorDriver.Create(new ViewGenerator()).RunGenerators(compilation).GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity is DiagnosticSeverity.Error);
        Assert.NotEmpty(result.GeneratedTrees);
        return string.Concat(result.GeneratedTrees.Select(tree => tree.ToString()));
    }
}
