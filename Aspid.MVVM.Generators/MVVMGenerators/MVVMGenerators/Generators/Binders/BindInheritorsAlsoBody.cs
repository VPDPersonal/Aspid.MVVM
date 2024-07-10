using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using System.Collections.Generic;
using MVVMGenerators.Descriptions;

namespace MVVMGenerators.Generators.Binders;

public static class BindInheritorsAlsoBody
{
    public static void AppendBindInheritorsAlso(this CodeWriter code, BinderData data)
    {
        if (!data.HasBindInheritorsAlsoInBaseType)
        {
            code.AppendBindMethod();
            code.AppendLine();
            code.AppendUnbindMethod();
            code.AppendLine();
        }

        code.AppendBindViaHandlerMethod(data.HasBindInheritorsAlsoInBaseType, data.BindInheritorsAlsoTypes);
    }

    private static void AppendBindMethod(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeBinderModelAttribute}}
              private readonly {{Classes.Dictionary.Global}}<{{Classes.Delegate.Global}}, {{Classes.Delegate.Global}}> _handlers = new();

              {{General.GeneratedCodeBinderModelAttribute}}
              bool {{Classes.IBinder.Global}}.Bind<T>(in T value, ref {{Classes.Action.Global}}<T> changed)
              {
                  switch (this)
                  {
                      case {{Classes.IBinder.Global}}<T> specificBinder:
                          specificBinder.SetValue(value);
                          changed += specificBinder.SetValue;
                          return true;
                          
                      case {{Classes.IAnyBinder.Global}} anyBinder:
                           anyBinder.SetValue(value);
                           changed += anyBinder.SetValue;
                           return true;
                           
                      default:
                          if (!BindViaHandler(value, out var handler)) return false;
                          changed += handler;
                          _handlers.Add(changed, handler);
                          return true;
                  }
              }
              """);
    }

    private static void AppendUnbindMethod(this CodeWriter code)
    {
        code.AppendMultiline(
            $$"""
              {{General.GeneratedCodeBinderModelAttribute}}
              bool {{Classes.IBinder.Global}}.Unbind<T>(ref {{Classes.Action.Global}}<T> changed)
              {
                 switch (this)
                 {
                     case {{Classes.IBinder.Global}}<T> specificBinder:
                         changed -= specificBinder.SetValue;
                         return true;
              
                     case {{Classes.IAnyBinder.Global}} anyBinder:
                         changed -= anyBinder.SetValue;
                         return true;
                         
                     default:
                         if (!_handlers.TryGetValue(changed, out var handler)) return false;
                         
                         changed -= ({{Classes.Action.Global}}<T>)handler;
                         _handlers.Remove(changed);
                         return true;
                 }
              }
              """);
    }

    private static void AppendBindViaHandlerMethod(this CodeWriter code, bool hasBaseType, IReadOnlyCollection<ITypeSymbol> types)
    {
        var method = hasBaseType ? "protected override " : "protected virtual ";
        method += $"bool BindViaHandler<T>(T value, out {Classes.Action.Global}<T> handler)";
        
        code.AppendLine(General.GeneratedCodeBinderModelAttribute)
            .AppendLine(method)
            .BeginBlock()
            .AppendLine(hasBaseType 
                ? "if (base.BindViaHandler<T>(value, out handler)) return false;" 
                : "handler = null;")
            .AppendLine();

        var i = 0;
        foreach (var type in types)
        {
            code.AppendMultiline(
                $$"""
                 if (value is {{type.ToDisplayString()}} value{{i}})
                 {
                    SetValue(value{{i}});
                    handler = param => SetValue(param as {{type.ToDisplayString()}});
                    return true;
                 }
                 """);

            code.AppendLine();
            i++;
        }
        
        code.AppendLine("return false;")
            .EndBlock()
            .EndBlock();
    }
}