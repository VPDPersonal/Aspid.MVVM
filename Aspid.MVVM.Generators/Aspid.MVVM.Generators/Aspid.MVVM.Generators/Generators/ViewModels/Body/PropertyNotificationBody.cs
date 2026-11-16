using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.MVVM.Generators.Shared;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Constants;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class PropertyNotificationBody
{
    internal static void Generate(
        in ViewModelData data,
        AspidTypeDeclaration declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        
        code.BeginScopedClass(declaration)
            .AppendBody(data)
            .EndScopedClass(declaration);

        context.AddSource(declaration.GetFileName(postfix: "PropertyNotification"), code.GetSourceText());
    }

    extension(CodeWriter code)
    {
        private CodeWriter AppendBody(in ViewModelData data)
        {
            return code
                .AppendOnPropertyChanged(data)
                .AppendLine()
                .AppendSetField(data);
        }

        private CodeWriter AppendOnPropertyChanged(in ViewModelData data)
        {
            var notificationData = data.PropertyNotificationData;

            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"private void OnPropertyChanged([{CallerLineNumberAttribute}] int line = -1)")
                .BeginBlock();

            if (notificationData.HasOnPropertyChangedCalls)
            {
                var first = true;
                
                foreach (var kvp in notificationData.OnPropertyChangedCalls)
                {
                    var propertyName = kvp.Key;
                    var lines = kvp.Value;
                    var linesPattern = string.Join(" or ", lines.Select(line => line.ToString()));
                    var keyword = first ? "if" : "else if";
                    
                    first = false;
                    code.AppendLine($"{keyword} (line is {linesPattern}) On{propertyName.CapitalizeFirstLetter()}PropertyChanged();");
                }
                
                code.AppendLine($"else throw new {NotImplementedException}($\"OnPropertyChanged: No property found for line {{line}}\");");
            }
            else
            {
                code.AppendLine($"throw new {NotImplementedException}($\"OnPropertyChanged: No property found for line {{line}}\");");
            }

            code.EndBlock()
                .AppendLine();

            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"private void OnPropertyChanged(string propertyName, [{CallerLineNumberAttribute}] int line = -1)")
                .BeginBlock()
                .AppendLine("switch (propertyName)")
                .BeginBlock();
            foreach (var name in notificationData.NamedOnPropertyChangedCalls.OrderBy(name => name))
            {
                var literal = Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(name, true);
                code.AppendLine($"case {literal}: On{name.CapitalizeFirstLetter()}PropertyChanged(); return;");
            }
            code.AppendLine("default: throw new global::System.ArgumentException(\"Unknown or unsupported notification property.\", nameof(propertyName));")
                .EndBlock()
                .EndBlock();

            return code;
        }

        private CodeWriter AppendSetField(in ViewModelData data)
        {
            var notificationData = data.PropertyNotificationData;

            code.AppendMultiline(
                $$"""
                {{GeneratedCodeViewModelAttribute}}
                private bool SetField<T>(ref T field, T newValue, [{{CallerLineNumberAttribute}}] int line = -1) =>
                    throw new {{NotImplementedException}}("SetField<T>: No property found for line {line}. Use typed overload.");
                    
                {{GeneratedCodeViewModelAttribute}}
                private bool SetField<T>(ref T field, T newValue, string propertyName, [{{CallerLineNumberAttribute}}] int line = -1) =>
                    throw new {{NotImplementedException}}($"SetField<T>: No property {propertyName} found for line {line}. Use typed overload.");
                """);
            
            if (notificationData.HasSetFieldCalls || notificationData.NamedSetFieldCallsByType.Count > 0)
            {
                foreach (var type in notificationData.SetFieldCallsByType.Keys
                    .Union(notificationData.NamedSetFieldCallsByType.Keys).OrderBy(type => type))
                {
                    var propertyCalls = notificationData.SetFieldCallsByType.TryGetValue(type, out var implicitCalls)
                        ? implicitCalls
                        : new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<int>>();

                    code.AppendLine(GeneratedCodeViewModelAttribute)
                        .AppendLine($"private bool SetField(ref {type} field, {type} newValue, [{CallerLineNumberAttribute}] int line = -1)")
                        .BeginBlock();

                    var first = true;
                    foreach (var kvp in propertyCalls)
                    {
                        var propertyName = kvp.Key;
                        var lines = kvp.Value;
                        var linesPattern = string.Join(" or ", lines.Select(l => l.ToString()));
                        var keyword = first ? "if" : "else if";
                        first = false;
                        
                        code.AppendLine($"{keyword} (line is {linesPattern}) return Set{propertyName.CapitalizeFirstLetter()}Field(ref field, newValue);");
                    }

                    var fallback = first ? "" : "else ";
                    code.AppendLine($"{fallback}throw new {NotImplementedException}($\"SetField: No property found for line {{line}}\");")
                        .EndBlock()
                        .AppendLine()
                        .AppendLine(GeneratedCodeViewModelAttribute)
                        .AppendLine($"private bool SetField(ref {type} field, {type} newValue, string propertyName, [{CallerLineNumberAttribute}] int line = -1)")
                        .BeginBlock()
                        .AppendLine("switch (propertyName)")
                        .BeginBlock();
                    if (notificationData.NamedSetFieldCallsByType.TryGetValue(type, out var names))
                    {
                        foreach (var name in names.OrderBy(name => name))
                        {
                            var literal = Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(name, true);
                            code.AppendLine($"case {literal}: return Set{name.CapitalizeFirstLetter()}Field(ref field, newValue);");
                        }
                    }
                    code.AppendLine("default: throw new global::System.ArgumentException(\"Unknown or unsupported notification property.\", nameof(propertyName));")
                        .EndBlock()
                        .EndBlock();
                }
            }
            
            return code;
        }
    }
}