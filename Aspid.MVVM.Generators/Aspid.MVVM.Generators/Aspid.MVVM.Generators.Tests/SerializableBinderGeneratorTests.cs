using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.SerializableBinders;
using Xunit;

namespace MVVMGenerators.Tests;

/// <summary>
/// Tests for the generator behind <c>[GenerateSerializableBinder]</c>.
/// </summary>
/// <remarks>
/// The MonoBehaviour half is the source: Unity resolves a component through a MonoScript asset, which exists only for a
/// type declared in a file of its own. What matters here is that the serializable half comes out of that one
/// declaration — body, serialized options and mode guard included — so the two cannot drift.
/// </remarks>
public sealed class SerializableBinderGeneratorTests
{
    private const string Framework = """
        using System;

        namespace Aspid.MVVM
        {
            public enum BindMode { None, OneTime, OneWay, OneWayToSource, TwoWay }

            [AttributeUsage(AttributeTargets.Class, Inherited = false)]
            public sealed class BindModeOverrideAttribute : Attribute
            {
                public bool IsAll { get; set; }
                public BindMode[] Modes { get; }
                public BindModeOverrideAttribute(params BindMode[] modes) { Modes = modes; }
            }
        }

        namespace UnityEngine
        {
            public class Object { }
            public class Component : Object { }
            public class Camera : Component { public float fieldOfView { get; set; } }
            public class SerializeField : Attribute { }
            public class TooltipAttribute : Attribute { public TooltipAttribute(string t) { } }
            public class AddComponentMenu : Attribute { public AddComponentMenu(string m) { } }
        }

        namespace Aspid.MVVM.StarterKit
        {
            [AttributeUsage(AttributeTargets.Class)]
            public sealed class GenerateSerializableBinderAttribute : Attribute { }

            public interface IConverter<TFrom, TTo> { }

            [Aspid.MVVM.BindModeOverride(Aspid.MVVM.BindMode.OneWay, Aspid.MVVM.BindMode.OneTime, Aspid.MVVM.BindMode.OneWayToSource)]
            public abstract class ComponentFloatMonoBinder<T> { protected T Target; protected T CachedComponent; }

            public abstract class TargetFloatBinder<T>
            {
                protected T Target;
                protected TargetFloatBinder(T target, IConverter<float, float> converter, Aspid.MVVM.BindMode mode = Aspid.MVVM.BindMode.OneWay) { }
            }

            public class GenericToStringConverter : IConverter<object, string> { }
            public abstract class MonoBinder { protected MonoBinder() { } }
            public abstract class Binder { protected Binder(Aspid.MVVM.BindMode mode) { } }

            public abstract class SwitcherMonoBinder<TComponent, T> { protected TComponent Target; protected TComponent CachedComponent; }

            public abstract class SwitcherBinder<TComponent, T>
            {
                protected TComponent Target;
                protected SwitcherBinder(TComponent target, T trueValue, T falseValue, IConverter<T, T> converter = null, Aspid.MVVM.BindMode mode = Aspid.MVVM.BindMode.OneWay) { }
            }
        }
        """;

    [Fact]
    public void TheSerializableHalfComesOutOfTheMonoHalf()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                [UnityEngine.AddComponentMenu("Aspid/MVVM/Binders/Camera – FOV")]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property
                    {
                        get => CachedComponent.fieldOfView;
                        set => CachedComponent.fieldOfView = value;
                    }
                }
            }
            """);

        Assert.Contains("class CameraFieldOfViewBinder : TargetFloatBinder<global::UnityEngine.Camera>", generated);
        Assert.Contains("get => Target.fieldOfView;", generated);
        Assert.Contains("set => Target.fieldOfView = value;", generated);
        Assert.DoesNotContain("CachedComponent", generated);
        Assert.DoesNotContain("AddComponentMenu", generated);
        Assert.Contains("[global::System.Serializable]", generated);
    }

    [Fact]
    public void TheConstructorIsSynthesised()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.Contains("public CameraFieldOfViewBinder(", generated);
        Assert.Contains("UnityEngine.Camera target,", generated);
        Assert.Contains("converter", generated);
        Assert.Contains("base(target, converter, mode)", generated);
    }

    [Fact]
    public void TheModeGuardComesFromTheBaseChain()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.Contains("mode.ThrowExceptionIfMatches(BindMode.TwoWay);", generated);
    }

    [Fact]
    public void ASerializedOptionBecomesAConstructorParameter()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    [UnityEngine.Tooltip("Whether the value is halved.")]
                    [UnityEngine.SerializeField] private bool _halve = true;

                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.Contains("bool halve = true", generated);
        Assert.Contains("_halve = halve;", generated);
        Assert.Contains("private bool _halve = true;", generated);
        Assert.Contains("Tooltip", generated);
    }

    [Fact]
    public void AHandWrittenTwin_IsLeftAlone()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }

                public class CameraFieldOfViewBinder : TargetFloatBinder<UnityEngine.Camera> { }
            }
            """);

        Assert.DoesNotContain("auto-generated", generated);
    }

    [Fact]
    public void AUnityLifecycleHook_IsNotCarriedOver()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }

                    private void OnValidate() { }
                }
            }
            """);

        Assert.DoesNotContain("OnValidate", generated);
    }

    /// <summary>
    /// The Switcher family takes its two values through the base constructor rather than a field of its own, so the
    /// constructor is mirrored from that base instead of assembled from a fixed shape.
    /// </summary>
    [Fact]
    public void ASwitcherFamily_MirrorsItsBaseConstructor()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewSwitcherMonoBinder : SwitcherMonoBinder<UnityEngine.Camera, float>
                {
                    protected override void SetValue(float value) => CachedComponent.fieldOfView = value;
                }
            }
            """);

        Assert.Contains("class CameraFieldOfViewSwitcherBinder : SwitcherBinder<", generated);
        Assert.Contains("trueValue", generated);
        Assert.Contains("falseValue", generated);
        Assert.Contains("base(target, trueValue, falseValue, converter, mode)", generated);
        Assert.Contains("Target.fieldOfView = value;", generated);
    }

    /// <summary>
    /// An interface the MonoBehaviour half implements describes the family, not the Inspector, so it belongs on both.
    /// </summary>
    [Fact]
    public void TheExtraInterfaces_AreCarriedOver()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                public interface IProbeBinder { }

                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>, IProbeBinder
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.Contains(", IProbeBinder", generated);
    }

    /// <summary>
    /// A ping helper takes the object to select in the Hierarchy. The MonoBehaviour half is that object and leaves the
    /// argument out; the serializable half is not one, so the target has to be named or the ping is lost.
    /// </summary>
    [Fact]
    public void APingHelper_IsHandedTheTarget()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property
                    {
                        get => CachedComponent.fieldOfView;
                        set
                        {
                            if (!this.RequireFinite(value)) return;
                            CachedComponent.fieldOfView = value;
                        }
                    }
                }
            }
            """);

        Assert.Contains("this.RequireFinite(value, Target)", generated);
    }

    /// <summary>
    /// Unity rebuilds a serialized instance without arguments, so the generated half offers a constructor for it —
    /// out of reach of calling code, which has nothing to put in a target or a mode.
    /// </summary>
    [Fact]
    public void ThereIsAConstructorForDeserialization()
    {
        var open = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        var closed = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public sealed class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.Contains("protected CameraFieldOfViewBinder() { }", open);
        Assert.Contains("private CameraFieldOfViewBinder() { }", closed);
        Assert.Contains("public sealed class CameraFieldOfViewBinder", closed);
    }

    /// <summary>
    /// The body is carried over from a half that is not written under a nullable context, so neither is the file it
    /// lands in: turning it on would declare every unannotated type non-nullable and clash with the base's own.
    /// </summary>
    [Fact]
    public void TheNullableContextIsOff()
    {
        var generated = Run("""
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.Contains("#nullable disable", generated);
        Assert.DoesNotContain("#nullable enable", generated);
        Assert.DoesNotContain("?", generated.Split("public CameraFieldOfViewBinder(")[1].Split(")")[0]);
    }

    /// <summary>
    /// A half that is compiled only under a condition produces one that is compiled under the same condition.
    /// </summary>
    [Fact]
    public void TheConditionalCompilationIsCarriedOver()
    {
        var generated = Run("""
            #if PROBE_INTEGRATION
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            #endif
            """);

        Assert.Contains("#if PROBE_INTEGRATION", generated);
        Assert.EndsWith("#endif\n", generated.Replace("\r\n", "\n"));
    }

    /// <summary>
    /// A condition that closes before the declaration says nothing about it — the header the package uses to define
    /// its profiler symbol is not carried over.
    /// </summary>
    [Fact]
    public void TheClosedConditionIsNotCarriedOver()
    {
        var generated = Run("""
            #if PROBE_INTEGRATION
            #define PROBE_PROFILER
            #endif
            namespace Aspid.MVVM.StarterKit
            {
                [GenerateSerializableBinder]
                public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<UnityEngine.Camera>
                {
                    protected sealed override float Property { get => CachedComponent.fieldOfView; set => CachedComponent.fieldOfView = value; }
                }
            }
            """);

        Assert.DoesNotContain("#if PROBE_INTEGRATION", generated);
        Assert.Contains("class CameraFieldOfViewBinder", generated);
    }

    /// <summary>The symbol the conditional-compilation test declares its half under.</summary>
    private static readonly CSharpParseOptions Options =
        CSharpParseOptions.Default.WithPreprocessorSymbols("PROBE_INTEGRATION");

    private static string Run(string declaration)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "Probe.Serializable",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(Framework, Options),
                CSharpSyntaxTree.ParseText(declaration, Options),
            },
            references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return string.Concat(CSharpGeneratorDriver
            .Create(new SerializableBinderGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .Results.SelectMany(result => result.GeneratedSources)
            .Select(source => source.SourceText.ToString()));
    }
}
