using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions;

namespace MVVMGenerators.Generators.ViewModels;

public static class ViewModelIdBody
{
    public static CodeWriter AppendViewModelId(this CodeWriter code, in ReadOnlySpan<IFieldSymbol> fields)
    {
        return code.AppendLoop(fields, field =>
        {
            var propertyName = field.GetPropertyName();
            code.AppendLine($"private const string {propertyName}Id = nameof({propertyName});");
        });
    }
}