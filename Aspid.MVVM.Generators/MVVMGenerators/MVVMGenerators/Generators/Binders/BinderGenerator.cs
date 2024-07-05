using System.Linq;
using System.Threading;
using System.Diagnostics;
using MVVMGenerators.Helpers;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Extensions.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Extensions.Declarations;

namespace MVVMGenerators.Generators.Binders;

[Generator]
public class BinderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(SyntacticPredicate, FindBinders)
            .Where(foundForSourceGenerator => foundForSourceGenerator.IsNeed)
            .Select((foundForSourceGenerator, _) => foundForSourceGenerator.Container);
        
        context.RegisterSourceOutput(
            source: provider,
            action: GenerateCode);
    }

    private static bool SyntacticPredicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        var candidate = node switch
        {
            ClassDeclarationSyntax or StructDeclarationSyntax => node as TypeDeclarationSyntax,
            _ => null
        };

        // Must implement IBinder or inherit from a type that inherits IBinder
        return candidate is not null
            && candidate.Modifiers.Any(SyntaxKind.PartialKeyword)
            && !candidate.Modifiers.Any(SyntaxKind.StaticKeyword)
            && candidate.BaseList is { Types.Count: > 0 };
    }

    private static FoundForGenerator<BinderData> FindBinders(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        
        if (symbol is null) return default;
        if (!symbol.HasInterface(Classes.IBinder, out var binderInterface)) return default;

        var hasBinderLogInBaseType = false; 
        var hasBindInheritorsAlsoInBaseType = false;
        
        for (var type = symbol; type != null; type = type.BaseType)
        {
            foreach (var method in type.GetMembers().OfType<IMethodSymbol>())
            {
                var methodsExplicitImplemented = binderInterface!.GetMembers().OfType<IMethodSymbol>()
                    .Any(binderMethod => binderMethod.EqualsSignature(method) && method.ExplicitInterfaceImplementations.Length != 0);
                    
                if (methodsExplicitImplemented) return default;

                if (!hasBinderLogInBaseType 
                    && !SymbolEqualityComparer.Default.Equals(type, symbol)
                    && method.HasAttribute(Classes.BinderLogAttribute))
                {
                    hasBinderLogInBaseType = true;
                }
                
                if (!hasBindInheritorsAlsoInBaseType 
                    && !SymbolEqualityComparer.Default.Equals(type, symbol)
                    && method.HasAttribute(Classes.BindInheritorsAlsoAttribute))
                {
                    hasBindInheritorsAlsoInBaseType = true;
                }
            }
        }
        
        var binderLogMethods = new List<IMethodSymbol>();
        var bindInheritorsAlsoTypes = new List<ITypeSymbol>();
        
        foreach (var method in symbol.GetMembers().OfType<IMethodSymbol>())
        {
            if (method.Parameters.Length != 1) continue;
            if (method.NameFromExplicitImplementation() != "SetValue") continue;
            if (!symbol.HasInterface($"{Classes.IBinder.FullName}<{method.Parameters[0].Type.ToDisplayString()}>")) continue;
            
            if (method.HasAttribute(Classes.BinderLogAttribute) &&
                !method.ExplicitInterfaceImplementations.Any())
                binderLogMethods.Add(method);
            
            if (method.HasAttribute(Classes.BindInheritorsAlsoAttribute))
                bindInheritorsAlsoTypes.Add(method.Parameters[0].Type);
        }
        
        if (binderLogMethods.Count + bindInheritorsAlsoTypes.Count == 0)
            return default;
        
        return new FoundForGenerator<BinderData>(true, 
            new BinderData(candidate, hasBinderLogInBaseType, binderLogMethods, hasBindInheritorsAlsoInBaseType, bindInheritorsAlsoTypes));
    }
    
    private static void GenerateCode(SourceProductionContext context, BinderData binderData)
    {
        var declaration = binderData.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        GenerateBinderLog(context, namespaceName, declarationText, binderData);
        GenerateBindInheritorsAlso(context, namespaceName, declarationText, binderData.HasBindInheritorsAlsoInBaseType, binderData.BindInheritorsAlsoTypes);
    }

    public static void GenerateBinderLog(
        SourceProductionContext context,
        string namespaceName,
        DeclarationText declarationText, 
        BinderData binderData)
    {
        if (binderData.BinderLogMethods.Count == 0) return;
        
        var code = new CodeWriter();
        code.AppendLine("#if UNITY_EDITOR")
            .AppendLine("// <auto-generated>");
        
        if (!string.IsNullOrEmpty(namespaceName))
        {
            code.AppendLine($"namespace {namespaceName}")
                .BeginBlock();   
        }

        code.AppendLine($"{declarationText}")
            .BeginBlock();
        
        code.AppendBinderLog(binderData);
        code.EndBlock();
        
        if (!string.IsNullOrEmpty(namespaceName))
            code.EndBlock();

        code.AppendLine("#endif");
        
        context.AddSource($"{declarationText.Name}.BinderLog.Generated.cs", code.GetSourceText());
    }

    public static void GenerateBindInheritorsAlso(
        SourceProductionContext context,
        string namespaceName,
        DeclarationText declarationText,
        bool hasBindInheritorsAlsoInBaseType,
        IReadOnlyCollection<ITypeSymbol> bindInheritorsAlsoTypes)
    {
        if (bindInheritorsAlsoTypes.Count == 0) return;
        
        var code = new CodeWriter();
        code.AppendLine("// <auto-generated>");
        
        if (!string.IsNullOrEmpty(namespaceName))
        {
            code.AppendLine($"namespace {namespaceName}")
                .BeginBlock();   
        }
        
        code.AppendLine($"{declarationText}")
            .BeginBlock();

        if (!hasBindInheritorsAlsoInBaseType)
        {
            
            code.AppendLine(
                    $"private readonly {Classes.Dictionary.Global}<{Classes.Delegate.Global}, {Classes.Delegate.Global}> _handlers = new();").
                AppendLine();

            code.
                AppendLine(
                    $"bool {Classes.IBinder.Global}.Bind<T>(in T value, ref {Classes.Action.Global}<T> changed)").
                BeginBlock().AppendLine("switch (this)").BeginBlock().
                AppendLine($"case {Classes.IBinder.Global}<T> specificBinder:").IncreaseIndent().
                AppendLine("specificBinder.SetValue(value);").AppendLine("changed += specificBinder.SetValue;").
                AppendLine("return true;").DecreaseIndent().AppendLine().
                AppendLine($"case {Classes.IAnyBinder.Global} anyBinder:").IncreaseIndent().
                AppendLine("anyBinder.SetValue(value);").AppendLine("changed += anyBinder.SetValue;").
                AppendLine("return true;").DecreaseIndent().AppendLine().AppendLine("default:").IncreaseIndent().
                AppendLine("if (!BindFromHandler(value, out var handler)) return false;").AppendLine().
                AppendLine("changed += handler;").AppendLine("_handlers.Add(changed, handler);").
                AppendLine("return true;").DecreaseIndent().EndBlock().EndBlock();

            code.AppendLine();

            code.AppendLine($"bool {Classes.IBinder.Global}.Unbind<T>(ref {Classes.Action.Global}<T> changed)").
                BeginBlock().AppendLine("switch (this)").BeginBlock().
                AppendLine($"case {Classes.IBinder.Global}<T> specificBinder:").IncreaseIndent().
                AppendLine("changed -= specificBinder.SetValue;").AppendLine("return true;").DecreaseIndent().
                AppendLine().AppendLine($"case {Classes.IAnyBinder.Global} anyBinder:").IncreaseIndent().
                AppendLine("changed -= anyBinder.SetValue;").AppendLine("return true;").DecreaseIndent().AppendLine().
                AppendLine("default:").IncreaseIndent().
                AppendLine("if (!_handlers.TryGetValue(changed, out var handler)) return false;").AppendLine().
                AppendLine($"changed -= ({Classes.Action.Global}<T>)handler;").AppendLine("_handlers.Remove(changed);").
                AppendLine("return true;").DecreaseIndent().EndBlock().EndBlock();
            
            code.AppendLine();
        }
        
        var bindFromHandlerMethod = hasBindInheritorsAlsoInBaseType ? "protected override " : "protected virtual ";
        bindFromHandlerMethod += "bool BindFromHandler<T>";

        code.AppendLine($"{bindFromHandlerMethod}(T value, out {Classes.Action.Global}<T> handler)")
            .BeginBlock();

        code.AppendLine(hasBindInheritorsAlsoInBaseType 
                ? "if (base.BindFromHandler<T>(value, out handler)) return false;" 
                : "handler = null;")
            .AppendLine();

        var i = 0;
        foreach (var type in bindInheritorsAlsoTypes)
        {
            code.AppendLine($"if (value is {type.ToDisplayString()} value{i})")
                .BeginBlock()
                .AppendLine($"SetValue(value{i});")
                .AppendLine($"handler = param => SetValue(param as {type.ToDisplayString()});")
                .AppendLine("return true;")
                .EndBlock()
                .AppendLine();

            i++;
        }

        code.AppendLine("return false;")
            .EndBlock()
            .EndBlock();
        
        if (!string.IsNullOrEmpty(namespaceName))
            code.EndBlock();
        
        context.AddSource($"{declarationText.Name}.BindInheritorsAlso.Generated.cs", code.GetSourceText());
    }
}