#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Generators.ViewModels;
using Xunit;

namespace MVVMGenerators.Tests;

public sealed class PropertyNotificationTests
{
    [Theory]
    [InlineData("SetAField(ref _a, value)")]
    [InlineData("SetField(ref _a, value)")]
    [InlineData("this.SetField(ref _a, value)")]
    [InlineData("SetField(ref _a, value, nameof(A))")]
    [InlineData("SetField(newValue: value, field: ref _a, propertyName: nameof(A))")]
    public void HelpersInAnotherPartialFileAssignAndNotify(string setter)
    {
        var type = Compile("", Property("A", setter));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 7);
        type.GetProperty("A")!.SetValue(model, 7);
        Assert.Equal(7, type.GetProperty("A")!.GetValue(model));
        Assert.Equal(1, type.GetField("AChanges")!.GetValue(model));
        Assert.Equal(0, type.GetField("BChanges")!.GetValue(model));
    }

    [Theory]
    [InlineData("OnPropertyChanged()")]
    [InlineData("OnPropertyChanged(nameof(A))")]
    [InlineData("OnPropertyChanged(propertyName: nameof(A), line: 999)")]
    public void NotificationInAnotherPartialFileResolvesItsProperty(string call)
    {
        var type = Compile("", Property("A", $"{{ _a = value; {call}; }}", block: true));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 7);
        Assert.Equal(1, type.GetField("AChanges")!.GetValue(model));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ConflictingImplicitSettersAreDiagnosedAtBothCalls(bool separateFiles)
    {
        var a = Property("A", "SetField(ref _a, value)");
        var b = Property("B", "SetField(ref _b, value)");
        var result = Run(separateFiles ? new[] { a, b } : new[] { a + b }).Result;
        Assert.Equal(2, result.Diagnostics.Count(diagnostic => diagnostic.Id == "ASPIDVM007"));
        Assert.All(result.Diagnostics.Where(diagnostic => diagnostic.Id == "ASPIDVM007"), diagnostic =>
            Assert.Contains("SetField", diagnostic.Location.SourceTree!.GetText().ToString(diagnostic.Location.SourceSpan)));
        Assert.Empty(result.GeneratedTrees);
    }

    [Fact]
    public void ConflictingImplicitNotificationsAreDiagnosed()
    {
        var result = Run(new[] { Property("A", "{ _a = value; OnPropertyChanged(); }", true),
            Property("B", "{ _b = value; OnPropertyChanged(); }", true) }).Result;
        Assert.Equal(2, result.Diagnostics.Count(diagnostic => diagnostic.Id == "ASPIDVM007"));
    }

    [Theory]
    [InlineData("SetField(ref _a, value, nameof(A))", "SetField(ref _b, value, nameof(B))")]
    [InlineData("SetField(ref _a, value)", "SetField(ref _b, value, nameof(B))")]
    [InlineData("SetAField(ref _a, value)", "SetBField(ref _b, value)")]
    public void ExplicitNamesAndDirectHelpersDisambiguateTheSameLine(string a, string b)
    {
        var type = Compile(Property("A", a) + Property("B", b));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("B")!.SetValue(model, 9);
        Assert.Equal(9, type.GetProperty("B")!.GetValue(model));
        Assert.Equal(0, type.GetField("AChanges")!.GetValue(model));
        Assert.Equal(1, type.GetField("BChanges")!.GetValue(model));
    }

    [Fact]
    public void ExplicitNotificationNameOverridesContainingPropertyAndLine()
    {
        var type = Compile(Property("A", "{ _a = value; OnPropertyChanged(nameof(B), line: 777); }", true)
            + Property("B", "SetBField(ref _b, value)"));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 9);
        Assert.Equal(0, type.GetField("AChanges")!.GetValue(model));
        Assert.Equal(1, type.GetField("BChanges")!.GetValue(model));
    }

    [Fact]
    public void DynamicExplicitNamesDispatchAndRejectUnknownNamesBeforeAssignment()
    {
        var type = Compile(Property("A", "SetAField(ref _a, value)") + Property("B", "SetBField(ref _b, value)"),
            "public bool Write(string name) => SetField(ref _b, 9, name); public void Notify(string name) => OnPropertyChanged(name);");
        var model = Activator.CreateInstance(type)!;
        var error = Assert.Throws<TargetInvocationException>(() => type.GetMethod("Write")!.Invoke(model, new object[] { "Missing" }));
        Assert.IsType<ArgumentException>(error.InnerException);
        Assert.Equal(0, type.GetProperty("B")!.GetValue(model));
        Assert.Equal(true, type.GetMethod("Write")!.Invoke(model, new object[] { "B" }));
        type.GetMethod("Notify")!.Invoke(model, new object[] { "A" });
        Assert.Equal(1, type.GetField("AChanges")!.GetValue(model));
        Assert.Equal(1, type.GetField("BChanges")!.GetValue(model));
    }

    [Theory]
    [InlineData("SetField(ref _a, value, line: 123)")]
    [InlineData("SetField(ref _a, value, 123)")]
    public void ExplicitNumericLineRemainsSupported(string setter)
    {
        var type = Compile(Property("A", setter));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 5);
        Assert.Equal(1, type.GetField("AChanges")!.GetValue(model));
    }

    [Theory]
    [InlineData("SetField(ref _a, value, \"Missing\")")]
    [InlineData("SetField(ref _a, value, propertyName: null)")]
    [InlineData("SetField<int>(ref _a, value)")]
    [InlineData("SetField(ref _a, value, line: value)")]
    public void UnsupportedCallsReportDiagnostics(string setter)
    {
        Assert.Contains(Run(new[] { Property("A", setter) }).Result.Diagnostics, diagnostic => diagnostic.Id == "ASPIDVM008");
    }

    [Fact]
    public void MappedLineNumbersMatchTheCallerLineNumberAttribute()
    {
        var type = Compile("\n#line 200\n" + Property("A", "SetField(ref _a, value)") + "\n#line default\n");
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 6);
        Assert.Equal(1, type.GetField("AChanges")!.GetValue(model));
    }

    [Fact]
    public void DifferentMappedFilesStillDiagnoseMatchingLineNumbers()
    {
        var result = Run(new[] { "\n#line 200 \"one.cs\"\n" + Property("A", "SetField(ref _a, value)") + "\n#line default\n",
            "\n#line 200 \"two.cs\"\n" + Property("B", "SetField(ref _b, value)") + "\n#line default\n" }).Result;
        Assert.Equal(2, result.Diagnostics.Count(diagnostic => diagnostic.Id == "ASPIDVM007"));
    }

    [Fact]
    public void NestedTypesAndForeignReceiversDoNotPolluteCallSites()
    {
        var result = Run(new[] { Property("A", "SetAField(ref _a, value)"),
            "class Nested { int A { get => 0; set { OnPropertyChanged(); } } void OnPropertyChanged() { } } " +
            "void Foreign(Nested other) { other.ToString(); }" }).Result;
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Id is "ASPIDVM007" or "ASPIDVM008");
    }

    [Fact]
    public void SourceOnlySetterDoesNotCallAMissingForwardNotification()
    {
        var type = Compile(Property("A", "SetField(ref _a, value)").Replace("[Bind]", "[Bind(BindMode.OneWayToSource)]"));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 8);
        Assert.Equal(8, type.GetProperty("A")!.GetValue(model));
        Assert.Equal(0, type.GetField("AChanges")!.GetValue(model));
    }

    [Fact]
    public void SameLineCallsWithDifferentFieldTypesHaveSeparateRoutes()
    {
        var type = Compile(Property("A", "SetField(ref _a, value)") +
            "[Bind] public string B { get => _text; set => SetField(ref _text, value); }");
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("B")!.SetValue(model, "text");
        Assert.Equal("text", type.GetProperty("B")!.GetValue(model));
        Assert.Equal(0, type.GetField("AChanges")!.GetValue(model));
        Assert.Equal(1, type.GetField("BChanges")!.GetValue(model));
    }

    [Fact]
    public void RepeatedCallsForTheSamePropertyAreNotAmbiguous()
    {
        var type = Compile(Property("A", "{ SetField(ref _a, value); SetField(ref _a, value); }", true));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("A")!.SetValue(model, 3);
        Assert.Equal(1, type.GetField("AChanges")!.GetValue(model));
    }

    [Fact]
    public void SameLineExplicitNotificationsSelectTheirOwnProperties()
    {
        var type = Compile(Property("A", "{ _a = value; OnPropertyChanged(nameof(A)); }", true) +
            Property("B", "{ _b = value; OnPropertyChanged(nameof(B)); }", true));
        var model = Activator.CreateInstance(type)!;
        type.GetProperty("B")!.SetValue(model, 4);
        Assert.Equal(0, type.GetField("AChanges")!.GetValue(model));
        Assert.Equal(1, type.GetField("BChanges")!.GetValue(model));
    }

    private static string Property(string name, string setter, bool block = false) =>
        $"[Bind] public int {name} {{ get => _{name.ToLowerInvariant()}; set {(block ? setter : "=> " + setter + ";")} }}";

    private static Type Compile(params string[] parts)
    {
        var (compilation, result) = Run(parts);
        Assert.DoesNotContain(result.Diagnostics, diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        var helpers = result.GeneratedTrees.Where(tree => tree.FilePath.EndsWith("PropertyNotification.g.cs")
            || tree.FilePath.EndsWith("GeneratedPropertyMethods.g.cs")).ToArray();
        Assert.Equal(2, helpers.Length);
        var notification = helpers.Single(tree => tree.FilePath.EndsWith("PropertyNotification.g.cs")).ToString();
        Assert.Contains("CallerLineNumber", notification);
        Assert.DoesNotContain("CallerMemberName", notification);
        Assert.All(helpers, tree => Assert.Empty(tree.GetRoot().DescendantNodes()
            .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax>()));
        compilation = compilation.AddSyntaxTrees(helpers);
        using var stream = new MemoryStream();
        var emit = compilation.Emit(stream);
        Assert.True(emit.Success, string.Join("\n", emit.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)));
        return Assembly.Load(stream.ToArray()).GetType("Model")!;
    }

    private static (CSharpCompilation Compilation, GeneratorDriverRunResult Result) Run(string[] parts)
    {
        var declarations = new[] { "using Aspid.MVVM; [ViewModel] public partial class Model { private int _a, _b; private string _text = \"\"; public int AChanges, BChanges; private void OnAPropertyChanged() { AChanges++; } private void OnBPropertyChanged() { BChanges++; } }" }
            .Concat(parts.Select(part => "using Aspid.MVVM; public partial class Model { " + part + " }"));
        var references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator)
            .Select(path => MetadataReference.CreateFromFile(path));
        var trees = declarations.Select((source, index) => CSharpSyntaxTree.ParseText(source, path: $"Part{index}.cs"))
            .Prepend(CSharpSyntaxTree.ParseText(ViewModelGeneratorTests.Framework));
        var compilation = CSharpCompilation.Create("Aspid.NotificationTests." + Guid.NewGuid().ToString("N"), trees,
            references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var result = CSharpGeneratorDriver.Create(new ViewModelGenerator()).RunGenerators(compilation).GetRunResult();
        Assert.All(result.Results, run => Assert.Null(run.Exception));
        return (compilation, result);
    }
}
