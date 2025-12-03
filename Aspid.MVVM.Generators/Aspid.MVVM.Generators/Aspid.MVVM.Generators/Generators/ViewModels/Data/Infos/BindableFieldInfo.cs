using System.Text;
using Microsoft.CodeAnalysis;
using Aspid.Generators.Helper;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Aspid.MVVM.Generators.Helpers;
using Aspid.MVVM.Generators.Generators.Ids.Data;
using static Aspid.Generators.Helper.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.Classes;
using static Aspid.MVVM.Generators.Generators.Descriptions.General;

namespace Aspid.MVVM.Generators.Generators.ViewModels.Data.Infos;

public sealed class BindableFieldInfo : IBindableMemberInfo
{
    public string Type { get; }
    
    public string Name { get; }
    
    public IdData Id { get; }
    
    public BindMode Mode { get; }
    
    public GeneratedBindableMembers Bindable { get; }
    
    public string Declaration { get; }

    public BindableFieldInfo(IFieldSymbol fieldSymbol, BindMode mode, ImmutableArray<BindableBindAlsoInfo> bindAlso)
    {
        Mode = mode;
        Id = new IdData(fieldSymbol);
        Type = fieldSymbol.Type.ToDisplayStringGlobal();
        Name = fieldSymbol.IsConst ? fieldSymbol.Name : fieldSymbol.GetPropertyName();
        Bindable = GeneratedBindableMembers.CreateForField(fieldSymbol);
        var accessors = Accessors.GetAccessors(fieldSymbol);

        var setMethodName = $"Set{Name}";
        StringBuilder declaration = new();
        
        if (!fieldSymbol.IsConst)
        {
            declaration.AppendLine($"#region {Name}");
            
            declaration.AppendLine(Mode is BindMode.OneTime
                ? $"""
                   {GeneratedCodeViewModelAttribute}
                   {accessors.General}{Type} {Name} => {fieldSymbol.Name};
                   """
                : $$"""
                    {{GeneratedCodeViewModelAttribute}}
                    {{accessors.General}}{{Type}} {{Name}}
                    {
                        {{accessors.Get}}get => {{fieldSymbol.Name}}; 
                        {{accessors.Set}}set => {{setMethodName}}(value); 
                    }
                    """);

            if (Mode is not BindMode.OneTime && Mode is not BindMode.None)
            {
                var onChangedMethod = $"On{Name}Changed";
                var onChangingMethod = $"On{Name}Changing";
                var methodModifier = Accessors.ConvertAccessorToString(accessors.SetKind);
                var keywordThis = !fieldSymbol.IsStatic ? "this." : string.Empty;

                var eventInvoke = Bindable.Invoke;

                foreach (var property in bindAlso)
                    eventInvoke += $"\n\t\t{property.Bindable.Invoke}";

                declaration.AppendLine();
                declaration.AppendLine(
                    $$"""
                      {{GeneratedCodeViewModelAttribute}} 
                      {{methodModifier}}void {{setMethodName}}({{Type}} value)
                      {
                          if ({{EqualityComparer_1}}<{{Type}}>.Default.Equals({{fieldSymbol.Name}}, value)) return;
                          
                          {{Type}} oldValue = {{fieldSymbol.Name}};
                          
                          {{onChangingMethod}}(value);
                          {{onChangingMethod}}(oldValue, value);
                          {
                              {{keywordThis}}{{fieldSymbol.Name}} = value;
                              {{eventInvoke}}
                          }
                          {{onChangedMethod}}(value);
                          {{onChangedMethod}}(oldValue, value);
                      }

                      {{GeneratedCodeViewModelAttribute}}
                      partial void {{onChangingMethod}}({{Type}} newValue);

                      {{GeneratedCodeViewModelAttribute}}
                      partial void {{onChangingMethod}}({{Type}} oldValue, {{Type}} newValue);

                      {{GeneratedCodeViewModelAttribute}}
                      partial void {{onChangedMethod}}({{Type}} newValue);

                      {{GeneratedCodeViewModelAttribute}}
                      partial void {{onChangedMethod}}({{Type}} oldValue, {{Type}} newValue);
                      """);
            }
            
            declaration.AppendLine("#endregion");
        }
        
        Declaration = declaration.ToString();
    }
    
    private readonly ref struct Accessors
    {
        public readonly SyntaxKind GetKind;
        public readonly SyntaxKind SetKind;
        
        public readonly string Get;
        public readonly string Set;
        public readonly string General;
    
        private Accessors(SyntaxKind get, SyntaxKind set)
        {
            GetKind = get;
            SetKind = set;
            
            switch (Compare(get, set))
            {
                case 0:
                    Get = string.Empty;
                    Set = string.Empty;
                    General = ConvertAccessorToString(get);
                    break;
            
                case > 0:
                    Get = string.Empty;
                    Set = ConvertAccessorToString(set);
                    General = ConvertAccessorToString(get);
                    break;
            
                case < 0:
                    Get = ConvertAccessorToString(get);
                    Set = string.Empty;
                    General = ConvertAccessorToString(set);
                    break;
            }
        }

        public static Accessors GetAccessors(IFieldSymbol field)
        {
            var get = SyntaxKind.PrivateKeyword;
            var set = SyntaxKind.PrivateKeyword;
        
            if (field.TryGetAnyAttributeInSelf(out var accessAttribute, AccessAttribute))
            {
                if (accessAttribute.ConstructorArguments.Length == 1)
                {
                    var value = (SyntaxKind)(int)(accessAttribute.ConstructorArguments[0].Value ?? SyntaxKind.PrivateKeyword);
                    get = value;
                    set = value;
                }

                foreach (var argument in accessAttribute!.NamedArguments)
                {
                    switch (argument.Key)
                    {
                        case "Get": get = (SyntaxKind)(int)(argument.Value.Value ?? SyntaxKind.PrivateKeyword); break;
                        case "Set": set = (SyntaxKind)(int)(argument.Value.Value ?? SyntaxKind.PrivateKeyword); break;
                    }
                }
            }

            return new Accessors(get, set);
        } 
        
        public static string ConvertAccessorToString(SyntaxKind syntaxKind) => syntaxKind switch
        {
            SyntaxKind.PublicKeyword => "public ",
            SyntaxKind.PrivateKeyword => "private ",
            SyntaxKind.ProtectedKeyword => "protected ",
            _ => string.Empty
        };
        
        private static int Compare(in SyntaxKind get, in SyntaxKind set)
        {
            if (get == set) return 0;

            if (set is SyntaxKind.PrivateKeyword) return 1;
            if (get is SyntaxKind.PrivateKeyword) return -1;
            
            if (set is SyntaxKind.ProtectedKeyword) return 1;
            if (get is SyntaxKind.ProtectedKeyword) return -1;

            // This is not impossible.
            return 0;
        }
    }
}
