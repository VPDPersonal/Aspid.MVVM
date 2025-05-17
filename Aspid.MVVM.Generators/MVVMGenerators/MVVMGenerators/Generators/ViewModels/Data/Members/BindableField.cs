using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Helpers.Extensions.Symbols;
using static MVVMGenerators.Helpers.Descriptions.General;
using static MVVMGenerators.Helpers.Descriptions.Classes;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public class BindableField : BindableMember<IFieldSymbol>
{
    public readonly bool IsReadOnly;
    public readonly string GetAccessAsText;
    public readonly string SetAccessAsText;
    public readonly string GeneralAccessAsText;
    public readonly ImmutableArray<BindableBindAlso> BindAlso;
    
    public BindableField(IFieldSymbol field, BindMode mode, ImmutableArray<BindableBindAlso> bindAlso)
        : base(field, 
            mode,
            field.Type.ToDisplayStringGlobal(), 
            field.Name, 
            mode is BindMode.OneTime ? field.Name : field.GetPropertyName(), 
            string.Empty)
    {
        BindAlso = bindAlso;
        IsReadOnly = mode is BindMode.OneTime;

        var accessors = GetAccessors(field);

        GetAccessAsText = accessors.Get == accessors.General
            ? string.Empty
            : ConvertAccessToText(accessors.Get);
        
        SetAccessAsText = accessors.Set == accessors.General
            ? string.Empty
            : ConvertAccessToText(accessors.Set);
        
        GeneralAccessAsText = ConvertAccessToText(accessors.General);
    }

    public string ToDeclarationPropertyString()
    {
        return IsReadOnly
            ? $"""
               {GeneratedCodeViewModelAttribute}
               {GeneralAccessAsText}{Type} {GeneratedName} => {SourceName};
               """
            : $$"""
                {{GeneratedCodeViewModelAttribute}}
                {{GeneralAccessAsText}}{{Type}} {{GeneratedName}}
                {
                    {{GetAccessAsText}}get => {{SourceName}};
                    {{SetAccessAsText}}set => Set{{GeneratedName}}(value);
                }
                """;
    }
    
    // TODO Nullable?
    public string ToSetMethodString()
    {
        if (Mode is BindMode.OneTime) return string.Empty;

        var setMethod = $"Set{GeneratedName}";
        var onMethodChanged = $"On{GeneratedName}Changed";
        var onMethodChanging = $"On{GeneratedName}Changing";

        var eventInvoke = Event.ToInvokeString();
        if (eventInvoke != string.Empty)
            eventInvoke = $"this.{eventInvoke}";
        
        var keyWordThis = !Member.IsStatic ? "this." : string.Empty;

        foreach (var property in BindAlso)
            eventInvoke += $"\n\tthis.{property.Event.ToInvokeString()}";

        return
            $$"""
              {{GeneratedCodeViewModelAttribute}}  
              private void {{setMethod}}({{Type}} value)
              {
                  if ({{EqualityComparer}}<{{Type}}>.Default.Equals({{SourceName}}, value)) return;

                  {{onMethodChanging}}({{SourceName}}, value);
                  {{keyWordThis}}{{SourceName}} = value;
                  {{eventInvoke}}
                  {{onMethodChanged}}(value);
              }

              {{GeneratedCodeViewModelAttribute}}
              partial void {{onMethodChanging}}({{Type}} oldValue, {{Type}} newValue);

              {{GeneratedCodeViewModelAttribute}}
              partial void {{onMethodChanged}}({{Type}} newValue);
              """;
    }

    private static string ConvertAccessToText(SyntaxKind syntaxKind) => syntaxKind switch
    {
        SyntaxKind.PrivateKeyword => "private ",
        SyntaxKind.ProtectedKeyword => "protected ",
        SyntaxKind.PublicKeyword => "public ",
        _ => ""
    };
    
    private static Accessors GetAccessors(IFieldSymbol field)
    {
        var accessors = new Accessors(SyntaxKind.PrivateKeyword, SyntaxKind.PrivateKeyword);
        if (!field.HasAttribute(AccessAttribute, out var accessAttribute)) return accessors;
            
        if (accessAttribute!.ConstructorArguments.Length == 1)
        {
            var value = (SyntaxKind)(int)(accessAttribute.ConstructorArguments[0].Value ?? SyntaxKind.PrivateKeyword);
            accessors.Get = value;
            accessors.Set = value;
        }
            
        foreach (var argument in accessAttribute!.NamedArguments)
        {
            switch (argument.Key)
            {
                case "Get": accessors.Get = (SyntaxKind)(int)(argument.Value.Value ?? SyntaxKind.PrivateKeyword); break;
                case "Set": accessors.Set = (SyntaxKind)(int)(argument.Value.Value ?? SyntaxKind.PrivateKeyword); break;
            }
        }

        return accessors;
    }
    
    private ref struct Accessors(SyntaxKind get, SyntaxKind set)
    {
        public SyntaxKind Get = get;
        public SyntaxKind Set = set;
        
        public SyntaxKind General => GetGeneralAccessor(Get, Set);
                
        private static SyntaxKind GetGeneralAccessor(SyntaxKind getAccessor, SyntaxKind setAccessor)
        {
            if (setAccessor == getAccessor) return getAccessor;
            if (getAccessor == SyntaxKind.PublicKeyword) return getAccessor;
            if (setAccessor == SyntaxKind.PublicKeyword) return setAccessor;
            if (getAccessor == SyntaxKind.ProtectedKeyword) return getAccessor;
            if (setAccessor == SyntaxKind.ProtectedKeyword) return setAccessor;

            return SyntaxKind.PrivateKeyword;
        }
    }
}