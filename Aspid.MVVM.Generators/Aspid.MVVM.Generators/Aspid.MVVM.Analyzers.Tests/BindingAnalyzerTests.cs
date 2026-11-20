using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspid.MVVM.Analyzers.FieldAnalyzers;
using Aspid.MVVM.Analyzers.TypeAnalyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Formatting;
using Xunit;

namespace Aspid.MVVM.Analyzers.Tests;

public sealed class BindingAnalyzerTests
{
    private const string Attributes = """
        namespace Aspid.MVVM
        {
            public class BindAttribute : System.Attribute { }
            public class ViewModelAttribute : System.Attribute { }
            public class ViewAttribute : System.Attribute { }
        }
        """;

    [Theory]
    [InlineData("_mana", "this._mana = 5;", "AM0001", "this.Mana = 5;")]
    [InlineData("m_mana", "m_mana++;", "AM0001", "Mana++;")]
    [InlineData("s_mana", "s_mana += 2;", "AM0001", "Mana += 2;")]
    [InlineData("mana", "--mana;", "AM0001", "--Mana;")]
    [InlineData("_mana", "System.Console.WriteLine(this._mana);", "AM0002", "this.Mana")]
    public async Task FieldFix_UsesGeneratedNameAndCompiles(string field, string body, string id, string expected)
    {
        using var workspace = new AdhocWorkspace();
        var document = CreateDocument(workspace, $$"""
            public class Player
            {
                [Aspid.MVVM.Bind] private int {{field}};
                public int Mana { get; set; }
                public void Update() { {{body}} }
            }
            """);
        var diagnostics = await Analyze(document, new BindAttributeAnalyzer());
        var diagnostic = Assert.Single(diagnostics);
        Assert.Equal(id, diagnostic.Id);
        Assert.Equal("Mana", diagnostic.Properties["PropertyName"]);
        var fixedDocument = await Fix(document, diagnostic, new BindAttributeCodeFixProvider());
        Assert.Contains(expected, (await fixedDocument.GetTextAsync()).ToString());
        Assert.Empty((await fixedDocument.Project.GetCompilationAsync())!.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error));
    }

    [Theory]
    [InlineData("System.Serializable, Aspid.MVVM.ViewModel", "class", "AM0005")]
    [InlineData("System.Serializable, Aspid.MVVM.View", "struct", "AM0004")]
    [InlineData("Aspid.MVVM.ViewModel, System.Serializable", "struct", "AM0006")]
    public async Task PartialFix_ExaminesEveryAttributeAndCompiles(string attributes, string kind, string id)
    {
        using var workspace = new AdhocWorkspace();
        var document = CreateDocument(workspace, $"[{attributes}] public {kind} Player {{ }}");
        var diagnostic = Assert.Single(await Analyze(document, new PartialAnalyzer()));
        Assert.Equal(id, diagnostic.Id);
        var fixedDocument = await Fix(document, diagnostic, new PartialCodeFixProvider());
        Assert.Empty(await Analyze(fixedDocument, new PartialAnalyzer()));
        Assert.Empty((await fixedDocument.Project.GetCompilationAsync())!.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error));
    }

    [Theory]
    [InlineData("Set(ref _mana);", "void Set(ref int value) { }")]
    [InlineData("Set(out _mana);", "void Set(out int value) { value = 1; }")]
    public async Task RefAndOutWrites_DoNotOfferInvalidPropertyFix(string body, string helper)
    {
        using var workspace = new AdhocWorkspace();
        var document = CreateDocument(workspace, $"class Player {{ [Aspid.MVVM.Bind] int _mana; void Update() {{ {body} }} {helper} }}");
        var diagnostic = Assert.Single(await Analyze(document, new BindAttributeAnalyzer()));
        Assert.Equal("AM0001", diagnostic.Id);
        Assert.Equal("false", diagnostic.Properties["CanFix"]);
        var actions = new List<CodeAction>();
        await new BindAttributeCodeFixProvider().RegisterCodeFixesAsync(new CodeFixContext(document, diagnostic, (a, _) => actions.Add(a), default));
        Assert.Empty(actions);
    }

    [Fact]
    public async Task ConstructorLambda_IsAnalyzedButDirectInitializationAndNameofAreNot()
    {
        using var workspace = new AdhocWorkspace();
        var document = CreateDocument(workspace, """
            class Player
            {
                [Aspid.MVVM.Bind] int _mana;
                Player() { _mana = 1; System.Action later = () => _mana++; }
                string Name => nameof(_mana);
                void Local() { int _mana = 1; _mana++; }
            }
            """);
        Assert.Equal("AM0001", Assert.Single(await Analyze(document, new BindAttributeAnalyzer())).Id);
    }

    [Fact]
    public async Task NestedStructWrite_HasNoInvalidPropertyFix()
    {
        using var workspace = new AdhocWorkspace();
        var document = CreateDocument(workspace, """
            struct Value { public int X; }
            class Player { [Aspid.MVVM.Bind] Value _value; void Update() { _value.X++; } }
            """);
        var diagnostic = Assert.Single(await Analyze(document, new BindAttributeAnalyzer()));
        Assert.Equal("AM0001", diagnostic.Id);
        Assert.Equal("false", diagnostic.Properties["CanFix"]);
    }

    private static Document CreateDocument(AdhocWorkspace workspace, string source)
    {
        var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        var project = workspace.AddProject("Tests", LanguageNames.CSharp)
            .WithCompilationOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .WithParseOptions(new CSharpParseOptions(LanguageVersion.CSharp10))
            .AddMetadataReferences(references);
        return project.AddDocument("Test.cs", source + "\n" + Attributes);
    }

    private static async Task<ImmutableArray<Diagnostic>> Analyze(Document document, DiagnosticAnalyzer analyzer) =>
        await (await document.Project.GetCompilationAsync())!.WithAnalyzers(ImmutableArray.Create(analyzer)).GetAnalyzerDiagnosticsAsync();

    private static async Task<Document> Fix(Document document, Diagnostic diagnostic, CodeFixProvider provider)
    {
        var actions = new List<CodeAction>();
        await provider.RegisterCodeFixesAsync(new CodeFixContext(document, diagnostic, (a, _) => actions.Add(a), default));
        var action = Assert.Single(actions);
        var operations = await action.GetOperationsAsync(CancellationToken.None);
        var solution = Assert.Single(operations.OfType<ApplyChangesOperation>()).ChangedSolution;
        return await Formatter.FormatAsync(solution.GetDocument(document.Id)!);
    }
}
