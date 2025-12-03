using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class PropertiesBody
{
    public static void Generate(
        string @namespace,
        in ViewModelData data,
        DeclarationText declaration,
        in SourceProductionContext context)
    {
        var code = new CodeWriter();

        code.BeginClass(@namespace, declaration)
            .AppendBody(data)
            .EndClass(@namespace);

        context.AddSource(declaration.GetFileName(@namespace, "Properties"), code.GetSourceText());
    }
    
    extension(CodeWriter code)
    {
        private CodeWriter AppendBody(in ViewModelData data)
        {
            if (!data.Members.IsEmpty)
            {
                code.AppendProperties(data)
                    .AppendBindableMembers(data);
            }
        
            return code.AppendNotifyAll(data);
        }

        private CodeWriter AppendBindableMembers(in ViewModelData data)
        {
            foreach (var member in data.Members)
            {
                code.AppendMultiline(member.Bindable.Declaration)
                    .AppendLine();
            }

            return code;
        }

        private CodeWriter AppendProperties(in ViewModelData data)
        {
            foreach (var field in data.Members.OfType<BindableFieldInfo>())
            {
                code.AppendMultiline(field.Declaration);
            }
        
            return code;
        }

        private CodeWriter AppendNotifyAll(in ViewModelData data)
        {
            var modifiers = "private";
            if (data.Inheritor is not Inheritor.None) modifiers = "protected override";
            else if (!data.Symbol.IsSealed) modifiers = "protected virtual";
        
            code.AppendLine(GeneratedCodeViewModelAttribute)
                .AppendLine($"{modifiers} void NotifyAll()")
                .BeginBlock()
                .AppendLineIf(data.Inheritor is Inheritor.Inheritor, "base.NotifyAll();");
        
            foreach (var member in data.Members)
            {
                var invoke = member.Bindable.Invoke;
                code.AppendLineIf(!string.IsNullOrWhiteSpace(invoke), invoke);
            }

            return code.EndBlock();
        }
    }
}