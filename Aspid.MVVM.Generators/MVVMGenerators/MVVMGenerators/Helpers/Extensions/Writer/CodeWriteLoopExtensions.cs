using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MVVMGenerators.Helpers.Extensions.Writer;

public static class CodeWriteLoopExtensions
{
    public static CodeWriter AppendLoop<T>(this CodeWriter code, IEnumerable<T> enumerable, Action<T> setValue)
    {
        foreach (var value in enumerable)
            setValue(value);

        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, IEnumerable<T> enumerable, Action<int, T> setValue)
    {
        var i = 0;
        
        foreach (var value in enumerable)
        {
            setValue(i, value);
            i++;
        }

        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, IEnumerable<T> enumerable, Action<CodeWriter, T> setValue)
    {
        foreach (var value in enumerable)
            setValue(code, value);

        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, IEnumerable<T> enumerable, Action<CodeWriter, int, T> setValue)
    {
        var i = 0;
        
        foreach (var value in enumerable)
        {
            setValue(code, i, value);
            i++;
        }

        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, ReadOnlySpan<T> span, Action<T> setValue)
    {
        foreach (var value in span)
            setValue(value);
        
        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, ReadOnlySpan<T> span, Action<int, T> setValue)
    {
        var i = 0;
        
        foreach (var value in span)
        {
            setValue(i, value);
            i++;
        }
    
        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, ReadOnlySpan<T> span, Action<CodeWriter, T> setValue)
    {
        foreach (var value in span)
            setValue(code, value);
    
        return code;
    }
    
    public static CodeWriter AppendLoop<T>(this CodeWriter code, ReadOnlySpan<T> span, Action<CodeWriter, int, T> setValue)
    {
        var i = 0;
        
        foreach (var value in span)
        {
            setValue(code, i, value);
            i++;
        }
    
        return code;
    }
}