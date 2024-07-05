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

        bool AreAnyExplicitlyImplementedMethods()
        {
            for (var type = symbol; type != null; type = type.BaseType)
            {
                foreach (var binderMethod in binderInterface!.GetMembers().OfType<IMethodSymbol>())
                {
                    var methodsExplicitImplemented = type.GetMembers().OfType<IMethodSymbol>()
                        .Where(method => binderMethod.EqualsSignature(method))
                        .Any(method => method.ExplicitInterfaceImplementations.Length != 0);

                    // TODO Add Diagnostic
                    // It should be noted that generation requires removing the explicit implementation of IBinder.
                    if (methodsExplicitImplemented) return true;
                }
            }

            return false;
        }
    }
    
    private static void GenerateCode(SourceProductionContext context, BinderData binderData)
    {
        var declaration = binderData.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        GenerateBinderLog(context, namespaceName, declarationText, binderData.HasBinderLogInBaseType, binderData.BinderLogMethods);
    }

    public static void GenerateBinderLog(
        SourceProductionContext context,
        string namespaceName,
        DeclarationText declarationText,
        bool hasBinderLogInBaseType,
        IReadOnlyCollection<IMethodSymbol> binderLogMethods)
    {
        if (binderLogMethods.Count == 0) return;
        
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

        if (!hasBinderLogInBaseType)
        {
            code.AppendLine($"[{Classes.SerializeFieldAttribute.AttributeGlobal}] protected bool IsDebug;").
                AppendLine().AppendLine("// TODO Custom Property").
                AppendLine(
                    $"[{Classes.SerializeFieldAttribute.AttributeGlobal}] private {Classes.List.Global}<string> _log;").
                AppendLine();
        }

        foreach (var method in binderLogMethods)
        {
            var parameterName = method.Parameters[0].Name;
            var parameterType = method.Parameters[0].Type.ToDisplayString();
            
            code.AppendLine(General.GeneratedCodeAttribute)
                .AppendLine($"void {Classes.IBinder.Global}<{parameterType}>.{method.Name}({parameterType} {parameterName})")
                .BeginBlock()
                .AppendLine("if (IsDebug)")
                .BeginBlock()
                .AppendLine("try")
                .BeginBlock()
                .AppendLine($"SetValue({parameterName});")
                .AppendLine($"AddLog($\"Set Value: {{{parameterName}}}\");")
                .EndBlock()
                .AppendLine($"catch ({Classes.Exception.Global} e)")
                .BeginBlock()
                .AppendLine($"AddLog($\"<color=red>Exception: {{e.Message}}. {{nameof({parameterName})}}: {parameterName}</color>\");")
                .AppendLine("throw;")
                .EndBlock()
                .EndBlock()
                .AppendLine($"else SetValue({parameterName});")
                .EndBlock()
                .AppendLine();
        }

        if (!hasBinderLogInBaseType)
        {
            code.AppendLine(General.GeneratedCodeAttribute)
                .AppendLine("protected void AddLog(string log)")
                .BeginBlock()
                .AppendLine($"_log ??= new {Classes.List.Global}<string>();")
                .AppendLine("_log.Add(log);")
                .EndBlock();
        }
        
        code.EndBlock();
        
        if (!string.IsNullOrEmpty(namespaceName))
            code.EndBlock();

        code.AppendLine("#endif");
        
        context.AddSource($"{declarationText.Name}.BinderLog.Generated.cs", code.GetSourceText());
    }
}