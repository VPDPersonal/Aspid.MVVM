using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.LeafBinders;
using Xunit;

namespace MVVMGenerators.Tests;

/// <summary>
/// Tests for the generator behind <c>[assembly: GenerateBinders]</c>.
/// </summary>
/// <remarks>
/// A leaf binder is four lines of real code wrapped in eighty that repeat. What matters here is that both halves come out
/// of one declaration — that is what makes them unable to drift — and that a declaration the package cannot honour fails
/// the build with a reason rather than emitting something that compiles and misbehaves.
/// </remarks>
public sealed class LeafBinderGeneratorTests
{
    private const string Attribute = """
        using System;

        namespace Aspid.MVVM.StarterKit
        {
            [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
            public sealed class GenerateBindersAttribute : Attribute
            {
                public Type Component { get; }
                public string Property { get; }
                public string Prefix { get; set; }
                public string Menu { get; set; }
                public string SerializedName { get; set; }

                public GenerateBindersAttribute(Type component, string property)
                {
                    Component = component;
                    Property = property;
                }
            }
        }
        """;

    private const string Component = """
        namespace Probe
        {
            public class Widget
            {
                public float Amount { get; set; }
                public string Label { get; }
                public System.DateTime Stamp { get; set; }
            }
        }
        """;

    [Fact]
    public void BothHalvesComeOutOfOneDeclaration()
    {
        var generated = Run("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Widget), "Amount",
                Prefix = "WidgetAmount",
                Menu = "Aspid/MVVM/Binders/Probe/Widget Binder – Amount",
                SerializedName = "m_Amount")]
            """);

        Assert.Contains("public class WidgetAmountBinder : TargetFloatBinder<Probe.Widget>", generated);
        Assert.Contains("public class WidgetAmountMonoBinder : ComponentFloatMonoBinder<Probe.Widget>", generated);

        Assert.Contains("get => Target.Amount;", generated);
        Assert.Contains("get => CachedComponent.Amount;", generated);
    }

    [Fact]
    public void TheMenuAndContextNamesReachTheGeneratedBinder()
    {
        var generated = Run("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Widget), "Amount",
                Menu = "Aspid/MVVM/Binders/Probe/Widget Binder – Amount",
                SerializedName = "m_Amount")]
            """);

        Assert.Contains("AddComponentMenu(\"Aspid/MVVM/Binders/Probe/Widget Binder – Amount\")", generated);
        Assert.Contains("serializePropertyNames: \"m_Amount\"", generated);
    }

    /// <summary>
    /// A guessed menu path would fail the package's own menu contract test, so a family without one is generated without
    /// a menu entry instead.
    /// </summary>
    [Fact]
    public void WithoutAMenu_NoMenuAttributeIsEmitted()
    {
        var generated = Run("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Widget), "Amount")]
            """);

        Assert.DoesNotContain("AddComponentMenu", generated);
        Assert.Contains("public class WidgetAmountMonoBinder", generated);
    }

    [Fact]
    public void APropertyThatDoesNotExist_FailsTheBuild()
    {
        var diagnostics = Diagnostics("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Widget), "Nothing")]
            """);

        Assert.Contains(diagnostics, diagnostic => diagnostic.Id is "ASPIDGB001");
    }

    /// <summary>
    /// The generic base would compile for any type and then behave unlike every other binder of that type, so an
    /// unsupported one is a build error naming the gap.
    /// </summary>
    [Fact]
    public void APropertyOfAnUnsupportedType_FailsTheBuild()
    {
        var diagnostics = Diagnostics("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Widget), "Stamp")]
            """);

        Assert.Contains(diagnostics, diagnostic => diagnostic.Id is "ASPIDGB002");
    }

    [Fact]
    public void AReadOnlyProperty_FailsTheBuild()
    {
        var diagnostics = Diagnostics("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Widget), "Label")]
            """);

        Assert.Contains(diagnostics, diagnostic => diagnostic.Id is "ASPIDGB003");
    }

    /// <summary>
    /// A bool family takes the inversion flag its base takes, not a converter — the two bases differ in that one
    /// argument, and a generator that emitted the same constructor for both would not compile.
    /// </summary>
    [Fact]
    public void ABoolFamily_TakesTheInversionFlag()
    {
        var generated = Run("""
            [assembly: Aspid.MVVM.StarterKit.GenerateBinders(typeof(Probe.Switch), "IsOn", Prefix = "SwitchIsOn")]
            """, """
            namespace Probe
            {
                public class Switch
                {
                    public bool IsOn { get; set; }
                }
            }
            """);

        Assert.Contains("bool isInvert = false", generated);
        Assert.Contains("base(target, isInvert, mode)", generated);
    }

    private static string Run(string declaration, string? component = null) =>
        string.Concat(Compile(declaration, component).Results
            .SelectMany(result => result.GeneratedSources)
            .Select(source => source.SourceText.ToString()));

    private static ImmutableArrayOfDiagnostics Diagnostics(string declaration) =>
        new(Compile(declaration, component: null).Diagnostics);

    private static GeneratorDriverRunResult Compile(string declaration, string? component)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "Probe.Generated",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(Attribute),
                CSharpSyntaxTree.ParseText(component ?? Component),
                CSharpSyntaxTree.ParseText(declaration),
            },
            references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return CSharpGeneratorDriver
            .Create(new LeafBinderGenerator())
            .RunGenerators(compilation)
            .GetRunResult();
    }

    /// <summary>
    /// Flattens the driver's diagnostics so a test can ask whether one was reported without repeating the shape.
    /// </summary>
    private readonly struct ImmutableArrayOfDiagnostics : System.Collections.Generic.IEnumerable<Diagnostic>
    {
        private readonly System.Collections.Immutable.ImmutableArray<Diagnostic> _diagnostics;

        public ImmutableArrayOfDiagnostics(System.Collections.Immutable.ImmutableArray<Diagnostic> diagnostics) =>
            _diagnostics = diagnostics;

        public System.Collections.Generic.IEnumerator<Diagnostic> GetEnumerator() =>
            ((System.Collections.Generic.IEnumerable<Diagnostic>)_diagnostics).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
