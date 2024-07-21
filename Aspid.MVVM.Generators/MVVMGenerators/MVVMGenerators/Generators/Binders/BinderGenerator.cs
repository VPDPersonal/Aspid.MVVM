using System.Linq;
using System.Threading;
using System.Diagnostics;
using MVVMGenerators.Helpers;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using MVVMGenerators.Helpers.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVVMGenerators.Helpers.Extensions.Writer;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions.Declarations;

namespace MVVMGenerators.Generators.Binders;

[Generator]
public class BinderGenerator : IIncrementalGenerator
{
    private const string PartialBinderLogName = "BinderLog.Generated";
    
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
        const string setValueName = "SetValue";
        
        for (var type = symbol; type != null; type = type.BaseType)
        {
            foreach (var method in type.GetMembers().OfType<IMethodSymbol>())
            {
                var methodsExplicitImplemented = binderInterface!.GetMembers().OfType<IMethodSymbol>()
                    .Any(binderMethod =>
                    {
                        if (binderMethod.Name is not setValueName) return false;
                        
                        return binderMethod.EqualsSignature(method) &&
                            method.ExplicitInterfaceImplementations.Length != 0;
                    });
                    
                if (methodsExplicitImplemented) return default;

                if (!hasBinderLogInBaseType 
                    && !SymbolEqualityComparer.Default.Equals(type, symbol)
                    && method.HasAttribute(Classes.BinderLogAttribute))
                {
                    hasBinderLogInBaseType = true;
                }
            }
        }
        
        var binderLogMethods = new List<IMethodSymbol>();
        
        foreach (var method in symbol.GetMembers().OfType<IMethodSymbol>())
        {
            if (method.Parameters.Length != 1) continue;
            if (method.NameFromExplicitImplementation() != setValueName) continue;
            if (!symbol.HasInterface($"{Classes.IBinder.FullName}<{method.Parameters[0].Type.ToDisplayString()}>")) continue;
            
            if (method.HasAttribute(Classes.BinderLogAttribute) &&
                !method.ExplicitInterfaceImplementations.Any())
                binderLogMethods.Add(method);
        }
        
        if (binderLogMethods.Count == 0) return default;
        
        return new FoundForGenerator<BinderData>(true, 
            new BinderData(candidate, hasBinderLogInBaseType, binderLogMethods));
    }
    
    private static void GenerateCode(SourceProductionContext context, BinderData binderData)
    {
        var declaration = binderData.Declaration;
        var namespaceName = declaration.GetNamespaceName();
        var declarationText = declaration.GetDeclarationText();

        GenerateBinderLog(context, binderData, namespaceName, declarationText);
    }

    private static void GenerateBinderLog(
        SourceProductionContext context, BinderData binderData,
        string namespaceName, DeclarationText declarationText)
    {
        if (binderData.BinderLogMethods.Count == 0) return;
        
        var code = new CodeWriter();

#if DEBUG
        code.AppendClass(namespaceName, declarationText, body: () => code.AppendBinderLog(binderData));
#else
        code.AppendLine($"#if {Defines.UNITY_EDITOR}")
            .AppendClass(namespaceName, declarationText, body: () => code.AppendBinderLog(binderData))
            .Append("#endif");
#endif
        
        context.AddSource($"{declarationText.Name}.{PartialBinderLogName}.cs", code.GetSourceText());
        
        #region Generation Example
        /*  #if UNITY_EDITOR
         *  namespace MyNamespace
         *  {
         *  |   public partial class MyClassName
         *  |   {
         *  |   |   [SerializeField] private bool _isDebug;
         *  |   |   [SerializeField] private List<string> _log;
         *  |   |
         *  |   |   protected bool IsDebug => _isDebug;
         *  |   |
         *  |   |   void IBinder<SomeType>.SetValue(SomeType value)
         *  |   |   {
         *  |   |   |   if (IsDebug)
         *  |   |   |   {
         *  |   |   |   |   try
         *  |   |   |   |   {
         *  |   |   |   |   |   SetValue(value);
         *  |   |   |   |   |   AddLog($"SetValue: {value}");
         *  |   |   |   |   }
         *  |   |   |   |   catch (Exception e)
         *  |   |   |   |   {
         *  |   |   |   |   |   AddLog($"<color=red>Exception: {e.Message}. {nameof(value)}: value</color>")
         *  |   |   |   |   |   throw;
         *  |   |   |   |   }
         *  |   |   |   }
         *  |   |   |   else SetValue(value);
         *  |   |   }
         *  |   |
         *  |   |   // Other SetValue methods
         *  |   |
         *  |   |   protected void AddLog(string log)
         *  |   |   {
         *  |   |   |   _log ??= new global::System.Collections.Generic.List<string>();
         *  |   |   |   _log.Add(log);
         *  |   |   }
         *  |   }
         *  }
         *  #endif
         */
        #endregion
    }
}