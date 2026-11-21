using System;
using System.Linq;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Ids;
using Aspid.MVVM.Generators.Generators.Views;
using Xunit;

namespace MVVMGenerators.Tests;

public sealed class ViewValidationTests
{
    private static readonly MetadataReference[] References = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
        .Split(System.IO.Path.PathSeparator).Select(path => MetadataReference.CreateFromFile(path)).ToArray();

    [Theory]
    [InlineData("null", "", "ASPIDVIEW001")]
    [InlineData("typeof(string)", "", "ASPIDVIEW001")]
    [InlineData("typeof(IBinder)", "", "ASPIDVIEW001")]
    [InlineData("typeof(ProbeBinder<>)", "public class ProbeBinder<T> : IBinder { public ProbeBinder(int target) { } }", "ASPIDVIEW001")]
    [InlineData("typeof(ProbeBinder)", "public abstract class ProbeBinder : IBinder { }", "ASPIDVIEW001")]
    [InlineData("typeof(ProbeBinder)", "public class ProbeBinder : IBinder { public ProbeBinder(string target) { } }", "ASPIDVIEW002")]
    [InlineData("typeof(ProbeBinder)", "public class ProbeBinder : IBinder { private ProbeBinder(int target) { } }", "ASPIDVIEW002")]
    public void InvalidBinderTypesAndConstructorsReportDiagnostics(string type, string binder, string id)
    {
        var compilation = Compilation($"[AsBinder({type})] private int _target;", binder);
        var result = Run(compilation);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == id && diagnostic.Severity is DiagnosticSeverity.Error);
        Assert.Empty(result.GeneratedTrees);
    }

    [Theory]
    [InlineData("[AsBinder(typeof(ProbeBinder), null)] private int _target;")]
    [InlineData("[AsBinder(typeof(ProbeBinder), 1)] private int _target;")]
    [InlineData("[AsBinder(typeof(ProbeBinder))] private int Target { set { } }")]
    public void InvalidArgumentsNeverCrashTheGenerator(string member)
    {
        var result = Run(Compilation(member, "public class ProbeBinder : IBinder { public ProbeBinder(int target, string text = null) { } }"));
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDVIEW002");
        Assert.Empty(result.GeneratedTrees);
    }

    [Theory]
    [InlineData("[View] public class TestView { }")]
    [InlineData("[View] public static partial class TestView { }")]
    public void InvalidViewDeclarationsReportAnError(string declaration)
    {
        var compilation = BaseCompilation("using Aspid.MVVM; " + declaration);
        var result = Run(compilation);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDVIEW003");
        Assert.Empty(result.GeneratedTrees);
    }

    [Theory]
    [InlineData("\"quote\\\" slash\\\\ newline\\n\"", "string")]
    [InlineData("'\\''", "char")]
    [InlineData("'\\\\'", "char")]
    [InlineData("1.25f", "float")]
    [InlineData("float.NaN", "float")]
    [InlineData("float.PositiveInfinity", "float")]
    [InlineData("-0.0f", "float")]
    [InlineData("double.NegativeInfinity", "double")]
    [InlineData("(short)1", "short")]
    [InlineData("1.25d", "double")]
    [InlineData("4294967295u", "uint")]
    [InlineData("9223372036854775807L", "long")]
    [InlineData("18446744073709551615UL", "ulong")]
    [InlineData("BindMode.OneTime", "BindMode")]
    [InlineData("typeof(string)", "System.Type")]
    [InlineData("(object)null", "string")]
    [InlineData("new int[] { 1, 2 }", "int[]")]
    public void GeneratedConstructorArgumentsCompileUnderACommaDecimalCulture(string argument, string parameterType)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            var compilation = Compilation($"[AsBinder(typeof(ProbeBinder), {argument})] private int _target;",
                $"public class ProbeBinder : IBinder {{ public ProbeBinder(int target, {parameterType} value) {{ }} }}");
            AssertCreationCompiles(compilation);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Fact]
    public void NameOfArgumentsReferToViewMembersIncludingExplicitArgumentArrays()
    {
        var compilation = Compilation("""
            private object Converter => new object();
            [AsBinder(arguments: new object[] { nameof(Converter) }, type: typeof(ProbeBinder))]
            private int _target;
            """, "public class ProbeBinder : IBinder { public ProbeBinder(int target, object converter) { } }");
        var creation = AssertCreationCompiles(compilation);
        Assert.EndsWith("(_target, Converter)", creation);
    }

    [Fact]
    public void InvalidAsBinderConfigurationDoesNotCrashTheIdGenerator()
    {
        var compilation = Compilation("[AsBinder(typeof(string), null)] private int _target;");
        var result = CSharpGeneratorDriver.Create(new ViewGenerator().AsSourceGenerator(), new IdGenerator().AsSourceGenerator())
            .RunGenerators(compilation).GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDVIEW001");
    }

    private static string AssertCreationCompiles(CSharpCompilation compilation)
    {
        var result = Run(compilation);
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity is DiagnosticSeverity.Error);
        var creation = Assert.Single(result.GeneratedTrees.SelectMany(tree => tree.GetRoot()
            .DescendantNodes().OfType<ObjectCreationExpressionSyntax>())
            .Where(expression => expression.Type.ToString().Contains("ProbeBinder"))).ToString();
        var probe = CSharpSyntaxTree.ParseText($"public partial class TestView {{ private void Probe() {{ _ = {creation}; }} }}");
        Assert.Empty(compilation.AddSyntaxTrees(probe).GetDiagnostics().Where(diagnostic => diagnostic.Severity is DiagnosticSeverity.Error));
        return creation;
    }

    private static GeneratorDriverRunResult Run(CSharpCompilation compilation)
    {
        var result = CSharpGeneratorDriver.Create(new ViewGenerator()).RunGenerators(compilation).GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        return result;
    }

    private static CSharpCompilation Compilation(string members, string binder = "") =>
        BaseCompilation($"using Aspid.MVVM; {binder} [View(AutoBinderFields = false)] public partial class TestView {{ {members} }}");

    private static CSharpCompilation BaseCompilation(string declaration) =>
        CSharpCompilation.Create("Probe.ViewValidation",
            new[] { CSharpSyntaxTree.ParseText(ViewGeneratorTests.Framework), CSharpSyntaxTree.ParseText(declaration) },
            References,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
}
