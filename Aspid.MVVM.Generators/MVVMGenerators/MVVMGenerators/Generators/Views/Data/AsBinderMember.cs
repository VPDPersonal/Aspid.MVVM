using Microsoft.CodeAnalysis;

namespace MVVMGenerators.Generators.Views.Data;

public readonly struct AsBinderMember<T>(T member, string binderType)
    where T : ISymbol
{
    public readonly T Member = member;
    public readonly string BinderType = binderType;
}