#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.ViewModels;
using Xunit;

namespace MVVMGenerators.Tests;

public sealed class CommandPredicateTests
{
    [Theory]
    [InlineData("private bool CanSave;", "", "CanSave = value;")]
    [InlineData("private bool CanSave { get; set; }", "", "CanSave = value;")]
    [InlineData("private bool CanSave() => enabled;", "", "enabled = value;")]
    [InlineData("private bool CanSave() => enabled;", "int value", "enabled = value;")]
    [InlineData("private bool CanSave(int value) => enabled;", "int value", "enabled = value;")]
    [InlineData("private bool CanSave<T>(T value) where T : struct => enabled;", "int value", "enabled = value;")]
    [InlineData("private bool CanSave(object value) => enabled;", "string value", "enabled = value;")]
    [InlineData("private bool CanSave(string value) => true; private bool CanSave(int value) => enabled;", "int value", "enabled = value;")]
    [InlineData("private static bool CanSave;", "", "CanSave = value;")]
    [InlineData("private bool CanSave;", "int a, int b", "CanSave = value;")]
    [InlineData("private bool CanSave;", "int a, int b, int c", "CanSave = value;")]
    [InlineData("private bool CanSave;", "int a, int b, int c, int d", "CanSave = value;")]
    [InlineData("private bool CanSave<T>(T value) where T : class => enabled;", "string value", "enabled = value;")]
    public void GeneratedCommandsCompileAndFollowPredicateChanges(string predicate, string parameters, string setter)
    {
        var arguments = string.Join(", ", parameters.Split(',').Where(parameter => parameter.Length > 0).Select(_ => "default"));
        var members = $$"""
            private bool enabled;
            private int executions;
            {{predicate}}
            [RelayCommand(CanExecute = "CanSave")] private void Save({{parameters}}) { executions++; }
            public void SetEnabled(bool value) { {{setter}} }
            public bool CanRun() => SaveCommand.CanExecute({{arguments}});
            public int Run() { SaveCommand.Execute({{arguments}}); return executions; }
            """;
        var model = CompileCommand(members);
        var instance = Activator.CreateInstance(model)!;
        foreach (var (enabled, count) in new[] { (false, 0), (true, 1), (false, 1) })
        {
            model.GetMethod("SetEnabled")!.Invoke(instance, new object[] { enabled });
            Assert.Equal(enabled, model.GetMethod("CanRun")!.Invoke(instance, null));
            Assert.Equal(count, model.GetMethod("Run")!.Invoke(instance, null));
        }
    }

    [Theory]
    [InlineData("private readonly bool CanSave = false;")]
    [InlineData("private const bool CanSave = false;")]
    [InlineData("private bool @event = false;", "event")]
    public void ReadOnlyAndKeywordFieldsCompile(string predicate, string name = "CanSave")
    {
        CompileCommand($"{predicate} [RelayCommand(CanExecute = \"{name}\")] private void Save() {{ }}");
    }

    [Theory]
    [InlineData("_")]
    [InlineData("predicate0")]
    [InlineData("event")]
    public void PredicateNamesDoNotCollideWithGeneratedParametersOrValidationLocals(string name)
    {
        CompileCommand($"private bool @{name}; [RelayCommand(CanExecute = \"{name}\")] private void Save(int value) {{ }}");
    }

    [Theory]
    [InlineData("protected bool CanSave;")]
    [InlineData("protected bool CanSave => false;")]
    [InlineData("protected bool CanSave() => false;")]
    public void AccessibleInheritedPredicatesCompile(string predicate)
    {
        CompileCommand("[RelayCommand(CanExecute = nameof(CanSave))] private void Save() { }", predicate);
    }

    [Theory]
    [InlineData("private bool CanSave<T>(int value) => true;", "int value")]
    [InlineData("private bool CanSave<T>(T value) where T : class => true;", "int value")]
    [InlineData("private bool CanSave<T>(string value) => true;", "int value")]
    [InlineData("private bool CanSave(ref int value) => true;", "int value")]
    [InlineData("private bool CanSave(out int value) { value = 0; return true; }", "int value")]
    [InlineData("private bool CanSave(in int value) => true;", "int value")]
    [InlineData("private int CanSave() => 1;", "")]
    [InlineData("private int CanSave;", "")]
    [InlineData("private bool? CanSave;", "")]
    [InlineData("private bool CanSave { set { } }", "")]
    [InlineData("private bool CanSave(System.IComparable value) => true; private bool CanSave(System.IFormattable value) => true;", "int value")]
    [InlineData("private new int CanSave => 1;", "", "protected bool CanSave => false;")]
    [InlineData("", "", "private bool CanSave => false;")]
    [InlineData("", "", "", "Missing")]
    [InlineData("", "", "", " ")]
    public void InvalidPredicatesReportAtTheAttribute(string predicate, string parameters, string inherited = "", string name = "CanSave")
    {
        var compilation = CreateCompilation($"{predicate} [RelayCommand(CanExecute = \"{name}\")] private void Save({parameters}) {{ }}", inherited);
        var result = CSharpGeneratorDriver.Create(new ViewModelGenerator()).RunGenerators(compilation).GetRunResult();
        var diagnostic = Assert.Single(result.Diagnostics.Where(diagnostic => diagnostic.Id == "ASPIDVM004"));
        Assert.Equal(DiagnosticSeverity.Error, diagnostic.Severity);
        Assert.Contains("RelayCommand", diagnostic.Location.SourceTree!.GetText().ToString(diagnostic.Location.SourceSpan));
        Assert.Empty(result.GeneratedTrees);
    }

    private static Type CompileCommand(string members, string inherited = "")
    {
        var compilation = CreateCompilation(members, inherited);
        var driver = CSharpGeneratorDriver.Create(new ViewModelGenerator()).RunGenerators(compilation);
        var result = driver.GetRunResult();
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        // Compile the emitted command members against the actual runtime command sources.
        var generated = result.GeneratedTrees.SelectMany(tree => tree.GetRoot().DescendantNodes())
            .OfType<ClassDeclarationSyntax>().SelectMany(type => type.Members)
            .Where(member => member is PropertyDeclarationSyntax property && property.Identifier.ValueText == "SaveCommand"
                || member is FieldDeclarationSyntax field && field.Declaration.Variables.Any(variable => variable.Identifier.ValueText == "__saveCommand"))
            .Select(member => member.WithoutTrivia().ToFullString());
        compilation = compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("public partial class Model { " + string.Join("\n", generated) + " }"));
        using var stream = new MemoryStream();
        var emit = compilation.Emit(stream);
        Assert.True(emit.Success, string.Join("\n", emit.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)));
        return Assembly.Load(stream.ToArray()).GetType("Model")!;
    }

    private static CSharpCompilation CreateCompilation(string members, string inherited)
    {
        var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        return CSharpCompilation.Create("Aspid.PredicateTest." + Guid.NewGuid().ToString("N"), new[]
        {
            CSharpSyntaxTree.ParseText(ViewModelGeneratorTests.Framework),
            CSharpSyntaxTree.ParseText($"using Aspid.MVVM; public class BaseModel {{ {inherited} }} [ViewModel] public partial class Model : BaseModel {{ {members} }}"),
            CSharpSyntaxTree.ParseText(ReadCommandSource("RelayCommand")),
            CSharpSyntaxTree.ParseText(ReadCommandSource("IRelayCommand")),
        }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private static string ReadCommandSource(string name)
    {
        using var stream = typeof(CommandPredicateTests).Assembly.GetManifestResourceStream($"Aspid.Tests.{name}.cs");
        Assert.NotNull(stream);
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }

}
