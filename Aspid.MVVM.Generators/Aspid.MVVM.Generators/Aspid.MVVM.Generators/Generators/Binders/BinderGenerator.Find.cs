using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Aspid.Generator.Helpers;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using Aspid.MVVM.Generators.Binders.Data;
using Aspid.MVVM.Generators.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Aspid.MVVM.Generators.Binders;

public partial class BinderGenerator
{
    private static FoundForGenerator<BinderData> FindBinders(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        
        if (symbol is null) return default;
        if (!symbol.HasInterfaceInSelfOrBases(Classes.IBinder, out var binderInterface)) return default;
        
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
                    && method.HasAnyAttribute(Classes.BinderLogAttribute))
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
            if (!symbol.HasInterfaceInSelfOrBases($"{Classes.IBinder.FullName}<{method.Parameters[0].Type.ToDisplayString()}>")) continue;
            
            if (method.HasAnyAttribute(Classes.BinderLogAttribute) &&
                !method.ExplicitInterfaceImplementations.Any())
                binderLogMethods.Add(method);
        }
        
        if (binderLogMethods.Count == 0) return default;
        
        return new FoundForGenerator<BinderData>(new BinderData(symbol, candidate, hasBinderLogInBaseType, binderLogMethods));
    }
}