using System.Linq;
using System.Threading;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Aspid.MVVM.Generators.Generators.Binders.Data;
using Classes = Aspid.MVVM.Generators.Generators.Descriptions.Classes;

namespace Aspid.MVVM.Generators.Generators.Binders;

public partial class BinderGenerator
{
    private static BinderData? FindBinders(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        Debug.Assert(context.Node is TypeDeclarationSyntax);
        var candidate = Unsafe.As<TypeDeclarationSyntax>(context.Node);
        var symbol = context.SemanticModel.GetDeclaredSymbol(candidate, cancellationToken);
        
        if (symbol is null) return null;
        if (!symbol.TryGetAnyInterfaceInSelfAndBases(out var binderInterface, Classes.IBinder)) return null;
        
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
                    
                if (methodsExplicitImplemented) return null;

                if (!hasBinderLogInBaseType 
                    && !SymbolEqualityComparer.Default.Equals(type, symbol)
                    && method.HasAnyAttributeInSelf(Classes.BinderLogAttribute))
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
            if (!symbol.HasAnyInterfaceInSelfAndBases($"{Classes.IBinder.FullName}<{method.Parameters[0].Type.ToDisplayString()}>")) continue;
            
            if (method.HasAnyAttributeInSelf(Classes.BinderLogAttribute) &&
                !method.ExplicitInterfaceImplementations.Any())
                binderLogMethods.Add(method);
        }

        return binderLogMethods.Count is 0
            ? null
            : new BinderData(symbol, candidate, hasBinderLogInBaseType, binderLogMethods);
    }
}