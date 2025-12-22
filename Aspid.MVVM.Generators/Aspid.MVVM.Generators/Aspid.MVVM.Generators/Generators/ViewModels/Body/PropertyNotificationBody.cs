using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class PropertyNotificationBody
{
    private const string CallerLineNumberAttribute = "[global::System.Runtime.CompilerServices.CallerLineNumber]";
    
    public static void Generate(
        string namespaceName,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();
        
        code.BeginClass(namespaceName, declaration)
            .AppendBody(data)
            .EndClass(namespaceName);

        context.AddSource(declaration.GetFileName(namespaceName, postfix: "PropertyNotification"), code.GetSourceText());
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

            code.AppendLine("#region OnPropertyChanged");
            
            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"private void OnPropertyChanged({CallerLineNumberAttribute} int line = -1)")
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
                    code.AppendLine($"{keyword} (line is {linesPattern}) On{propertyName}PropertyChanged();");
                }
                
                code.AppendLine($"else throw new {NotImplementedException}($\"OnPropertyChanged: No property found for line {{line}}\");");
            }
            else
            {
                code.AppendLine($"throw new {NotImplementedException}($\"OnPropertyChanged: No property found for line {{line}}\");");
            }

            code.EndBlock()
                .AppendLine();

            code.AppendMultiline(
                $"""
                {GeneratedCodeViewModelAttribute}
                private void OnPropertyChanged(string propertyName, {CallerLineNumberAttribute} int line = -1) =>
                    OnPropertyChanged(line);
                """)
                .AppendLine("#endregion");

            return code;
        }

        private CodeWriter AppendSetField(in ViewModelData data)
        {
            var notificationData = data.PropertyNotificationData;

            code.AppendLine("#region SetField")
                .AppendMultiline(
                $$"""
                {{GeneratedCodeViewModelAttribute}}
                private bool SetField<T>(ref T field, T newValue, {{CallerLineNumberAttribute}} int line = -1) =>
                    throw new {{NotImplementedException}}("SetField<T>: No property found for line {line}. Use typed overload.");
                    
                {{GeneratedCodeViewModelAttribute}}
                private bool SetField<T>(ref T field, T newValue, string propertyName, {{CallerLineNumberAttribute}} int line = -1) =>
                    throw new {{NotImplementedException}}($"SetField<T>: No property {propertyName} found for line {line}. Use typed overload.");
                """);
            
            if (notificationData.HasSetFieldCalls)
            {
                foreach (var typeGroup in notificationData.SetFieldCallsByType)
                {
                    var type = typeGroup.Key;
                    var propertyCalls = typeGroup.Value;

                    code.AppendLine(GeneratedCodeViewModelAttribute)
                        .AppendLine($"private bool SetField(ref {type} field, {type} newValue, {CallerLineNumberAttribute} int line = -1)")
                        .BeginBlock();

                    var first = true;
                    foreach (var kvp in propertyCalls)
                    {
                        var propertyName = kvp.Key;
                        var lines = kvp.Value;
                        var linesPattern = string.Join(" or ", lines.Select(l => l.ToString()));
                        var keyword = first ? "if" : "else if";
                        first = false;
                        
                        code.AppendLine($"{keyword} (line is {linesPattern}) return Set{propertyName}Field(ref field, newValue);");
                    }

                    code.AppendLine($"else throw new {NotImplementedException}($\"SetField: No property found for line {{line}}\");")
                        .EndBlock()
                        .AppendLine()
                        .AppendMultiline(
                            $"""
                            {GeneratedCodeViewModelAttribute}
                            private bool SetField(ref {type} field, {type} newValue, string propertyName, {CallerLineNumberAttribute} int line = -1) =>
                                SetField(ref field, newValue, line);
                            """);
                }
            }
            
            return  code.AppendLine("#endregion");
        }
    }
}

