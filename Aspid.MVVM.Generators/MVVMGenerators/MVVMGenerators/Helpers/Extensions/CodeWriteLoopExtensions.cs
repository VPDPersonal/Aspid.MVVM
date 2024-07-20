using System;
using System.Collections.Generic;

namespace MVVMGenerators.Helpers.Extensions;

public static class CodeWriteLoopExtensions
{
    public static CodeWriter AppendLoop<T>(this CodeWriter code, IEnumerable<T> enumerable, Action<T> setValue)
    {
        foreach (var value in enumerable)
            setValue(value);

        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, ReadOnlySpan<T> enumerable, Action<T> setValue)
    {
        foreach (var value in enumerable)
            setValue(value);
    
        return code;
    }
}