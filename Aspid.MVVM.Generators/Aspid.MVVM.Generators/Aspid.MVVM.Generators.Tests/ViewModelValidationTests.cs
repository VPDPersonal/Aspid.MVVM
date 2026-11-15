using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.Ids;
using Aspid.MVVM.Generators.Generators.Views;
using Aspid.MVVM.Generators.Generators.ViewModels;
using Xunit;

namespace MVVMGenerators.Tests;

public sealed class ViewModelValidationTests
{
    [Theory]
    [InlineData("[Bind(BindMode.None)] private int _value;", "ASPIDVM001")]
    [InlineData("[Bind((BindMode)99)] private int _value;", "ASPIDVM001")]
    [InlineData("[Bind, OneWayBind] private int _value;", "ASPIDVM001")]
    [InlineData("[Bind, Access((Access)0)] private int _value;", "ASPIDVM002")]
    [InlineData("[Bind, Access(Get = (Access)0)] private int _value;", "ASPIDVM002")]
    [InlineData("[Bind, BindAlso(\"Missing\")] private int _value;", "ASPIDVM003")]
    [InlineData("[Bind, BindAlso(\" \" )] private int _value;", "ASPIDVM003")]
    [InlineData("[Bind, BindAlso(null)] private int _value;", "ASPIDVM003")]
    [InlineData("[RelayCommand(CanExecute = \"Missing\")] private void Save() { }", "ASPIDVM004")]
    [InlineData("[RelayCommand(CanExecute = \" \" )] private void Save() { }", "ASPIDVM004")]
    [InlineData("private int CanSave => 1; [RelayCommand(CanExecute = nameof(CanSave))] private void Save() { }", "ASPIDVM004")]
    [InlineData("private bool CanSave(string value) => true; [RelayCommand(CanExecute = nameof(CanSave))] private void Save(int value) { }", "ASPIDVM004")]
    [InlineData("private bool CanSave { set { } } [RelayCommand(CanExecute = nameof(CanSave))] private void Save() { }", "ASPIDVM004")]
    [InlineData("[RelayCommand] private int Save() => 1;", "ASPIDVM005")]
    [InlineData("[RelayCommand] private void Save<T>(T value) { }", "ASPIDVM005")]
    [InlineData("[RelayCommand] private void Save(ref int value) { }", "ASPIDVM005")]
    [InlineData("[RelayCommand] private void Save(int a, int b, int c, int d, int e) { }", "ASPIDVM005")]
    [InlineData("[RelayCommand, RelayCommand] private void Save() { }", "ASPIDVM005")]
    public void InvalidConfigurationsReportAnErrorWithoutGeneratingAViewModel(string members, string id)
    {
        var result = Run(members);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == id && diagnostic.Severity == DiagnosticSeverity.Error);
        Assert.Empty(result.GeneratedTrees);
    }

    [Theory]
    [InlineData("[Bind(BindMode.OneWay)] private readonly int _value;")]
    [InlineData("[Bind(BindMode.OneWay)] private const int Value = 1;")]
    [InlineData("[Bind(BindMode.TwoWay)] private int Value => 1;")]
    [InlineData("[Bind(BindMode.OneWay)] private int Value { set { } }")]
    [InlineData("[OneTimeBind, BindAlso(nameof(Label))] private int _value; private string Label => \"label\";")]
    [InlineData("[Bind, BindAlso(nameof(Label))] private int _value; [OneTimeBind] private string Label => \"label\";")]
    public void UnsupportedBindingsRemainSkippedWithAWarning(string members)
    {
        var result = Run(members);
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDVM006" && diagnostic.Severity == DiagnosticSeverity.Warning);
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        Assert.NotEmpty(result.GeneratedTrees);
    }

    [Theory]
    [InlineData("private bool CanSave => false;", "", ", () => CanSave")]
    [InlineData("private bool CanSave() => false;", "", ", CanSave")]
    [InlineData("private bool CanSave() => false;", "int value", ", (_) => CanSave()")]
    [InlineData("private bool CanSave(int value) => false;", "int value", ", CanSave")]
    [InlineData("private bool CanSave(object value) => false;", "string value", ", CanSave")]
    [InlineData("[Bind] private bool _canSave;", "", ", () => CanSave")]
    public void ValidPredicatesAreAlwaysPresentInTheGeneratedCommand(string predicate, string parameters, string expected)
    {
        var result = Run($"{predicate} [RelayCommand(CanExecute = \"CanSave\")] private void Save({parameters}) {{ }}");
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        Assert.Contains(expected, string.Concat(result.GeneratedTrees.Select(tree => tree.ToString())));
    }

    [Fact]
    public void GeneratedLookupsValidateQueriesBeforeReadingTheirIdentifier()
    {
        var result = Run("[Bind] private int _aa; [Bind] private int _bb;");
        var lookup = result.GeneratedTrees.Single(tree => tree.ToString().Contains("FindBindableMember(in")).ToString();
        Assert.True(lookup.IndexOf("parameters.EnsureValid();") < lookup.IndexOf("parameters.Id.Length"));
    }

    [Fact]
    public void InheritedAccessiblePredicatesAreSupported()
    {
        var result = Run("[RelayCommand(CanExecute = nameof(CanSave))] private void Save() { }",
            "public class BaseModel { protected bool CanSave => false; }");
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        Assert.Contains(", () => CanSave", string.Concat(result.GeneratedTrees.Select(tree => tree.ToString())));
    }

    [Theory]
    [InlineData("[Bind((BindMode)99)] private int _value;", "ASPIDVM001")]
    [InlineData("[Bind, Access((Access)0)] private int _value;", "ASPIDVM002")]
    [InlineData("[RelayCommand(CanExecute = \"Missing\")] private void Save() { }", "ASPIDVM004")]
    public void InvalidModelsDoNotCrashTheViewOrIdGenerators(string members, string id)
    {
        const string views = """
            namespace Aspid.MVVM
            {
                public interface IView { }
                public interface IView<T> : IView where T : IViewModel { }
                [System.AttributeUsage(System.AttributeTargets.Class)]
                public sealed class ViewAttribute : System.Attribute { }
            }
            [Aspid.MVVM.View]
            public partial class TestView : Aspid.MVVM.IView<Model> { }
            """;
        var compilation = CreateCompilation(members).AddSyntaxTrees(CSharpSyntaxTree.ParseText(views));
        var result = CSharpGeneratorDriver.Create(
                new ViewModelGenerator().AsSourceGenerator(),
                new ViewGenerator().AsSourceGenerator(),
                new IdGenerator().AsSourceGenerator())
            .RunGenerators(compilation).GetRunResult();

        Assert.All(result.Results, run => Assert.Null(run.Exception));
        Assert.Contains(result.Diagnostics, diagnostic => diagnostic.Id == id);
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Id == "CS8785");
    }

    private static GeneratorDriverRunResult Run(string members, string baseClass = "public class BaseModel { }")
    {
        var compilation = CreateCompilation(members, baseClass);
        var result = CSharpGeneratorDriver.Create(new ViewModelGenerator()).RunGenerators(compilation).GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        return result;
    }

    private static CSharpCompilation CreateCompilation(string members, string baseClass = "public class BaseModel { }") =>
        CSharpCompilation.Create("Probe.ViewModelValidation",
            new[]
            {
                CSharpSyntaxTree.ParseText(ViewModelGeneratorTests.Framework),
                CSharpSyntaxTree.ParseText($"using Aspid.MVVM; {baseClass} [ViewModel] public partial class Model : BaseModel {{ {members} }}"),
            },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
}
