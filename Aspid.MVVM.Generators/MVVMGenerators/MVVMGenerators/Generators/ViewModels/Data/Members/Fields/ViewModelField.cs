using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Generators.Ids;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members.Fields;

public readonly struct ViewModelField(
    BindMode mode,
    IFieldSymbol field,
    ImmutableArray<BindAlsoProperty> bindAlso)
{
    public readonly string FieldName = field.Name;
    public readonly string PropertyName = field.GetPropertyName();
    public readonly string BindId = field.GetId(field.GetPropertyName());
    
    public readonly bool IsReadOnly = mode is BindMode.OneTime;
    public readonly string Type = field.Type.ToDisplayStringGlobal();

    public readonly BindMode Mode = mode;
    public readonly ViewModelEvent Event = new(mode, field);
    public readonly ImmutableArray<BindAlsoProperty> BindAlso = bindAlso;

    private readonly Accessors _accessors = GetAccessors(field);

    public string GetAccessAsText => _accessors.Get == _accessors.General 
            ? string.Empty 
            : ConvertAccessToText(_accessors.Get);
    
    public string SetAccessAsText => _accessors.Set == _accessors.General 
        ? string.Empty 
        : ConvertAccessToText(_accessors.Set);

    public string GeneralAccessAsText => ConvertAccessToText(_accessors.General);
    
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
        if (!field.HasAttribute(Classes.AccessAttribute, out var accessAttribute)) return accessors;
            
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
    
    private struct Accessors(SyntaxKind get, SyntaxKind set)
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