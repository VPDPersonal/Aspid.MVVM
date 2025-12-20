using System.Linq;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Generic;
using Aspid.MVVM.Generators.Generators.ViewModels.Data;
using Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;
using BindMode = Aspid.MVVM.Generators.Generators.ViewModels.Data.BindMode;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Body;

public static class BindableMembers
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

        context.AddSource(declaration.GetFileName(@namespace, "BindableMembers"), code.GetSourceText());
    }
    
    private static IEnumerable<IBindableMemberInfo> GetBindableBindAlso(ISymbol member, in ViewModelData data)
    {
        var set = new HashSet<string>();

        foreach (var attribute in member.GetAttributes())
        {
            if (attribute.AttributeClass is not null &&
                attribute.AttributeClass.ToDisplayStringGlobal() == BindAlsoAttribute)
            {
                var value = attribute.ConstructorArguments[0].Value;
                if (value is null) continue;

                set.Add(value.ToString());
            }
        }

        return data.Members.Where(bindAlso => 
            set.Contains(bindAlso.Name) && bindAlso.Mode is not BindMode.OneTime and not BindMode.None);
    }
    
    extension(CodeWriter code)
    {
        private CodeWriter AppendBody(in ViewModelData data)
        {
            if (!data.Members.IsEmpty)
            {
                code.AppendBindableMembers(data);
            }
        
            return code.AppendNotifyAll(data);
        }

        private CodeWriter AppendBindableMembers(in ViewModelData data)
        {
            foreach (var member in data.Members)
            {
                code.AppendLine($"#region {member.Name}")
                    .AppendMultiline(member.Bindable.Declaration)
                    .AppendLine();

                if (!string.IsNullOrWhiteSpace(member.Bindable.OnPropertyChangedName))
                {
                    code.AppendLine($"{GeneratedCodeViewModelAttribute}")
                        .AppendLine($"private void On{member.Name}PropertyChanged()")
                        .BeginBlock()
                        .AppendLineIf(!string.IsNullOrWhiteSpace(member.Bindable.Invoke), member.Bindable.Invoke);
                    
                    foreach (var bindAlso in GetBindableBindAlso(member.Member, data))
                    {
                        code.AppendLineIf(!string.IsNullOrWhiteSpace(member.Bindable.OnPropertyChangedName), $"{bindAlso.Bindable.OnPropertyChangedName}();");
                    }

                    code.EndBlock();
                }
                
                code.AppendLine("#endregion")
                    .AppendLine();
            }

            return code;
        }

        private CodeWriter AppendNotifyAll(in ViewModelData data)
        {
            var modifiers = "public";
            if (data.Inheritor is not Inheritor.None) modifiers += " override";
            else if (!data.Symbol.IsSealed) modifiers += " virtual";
        
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