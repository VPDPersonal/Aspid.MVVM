using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Helpers.Extensions.Symbols;

namespace MVVMGenerators.Generators.ViewModels.Data.Members;

public sealed class BindableField : BindableMember
{
    public readonly bool IsReadOnly;
    public readonly ViewModelEvent Event;
    public readonly string GetAccessAsText;
    public readonly string SetAccessAsText;
    public readonly string GeneralAccessAsText;
    public readonly ImmutableArray<BindableBindAlso> BindAlso;
    
    public override string Type { get; }
    
    public BindableField(IFieldSymbol field, BindMode mode, ImmutableArray<BindableBindAlso> bindAlso)
        : base(field, mode, field.Name, field.GetPropertyName())
    {
        BindAlso = bindAlso;
        IsReadOnly = mode is BindMode.OneTime;
        Event = new ViewModelEvent(mode, field);
        Type = field.Type.ToDisplayStringGlobal();

        var accessors = GetAccessors(field);

        GetAccessAsText = accessors.Get == accessors.General
            ? string.Empty
            : ConvertAccessToText(accessors.Get);
        
        SetAccessAsText = accessors.Set == accessors.General
            ? string.Empty
            : ConvertAccessToText(accessors.Set);
        
        GeneralAccessAsText = ConvertAccessToText(accessors.General);
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