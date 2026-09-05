---
title: "Class NullCoalesceConverter<T>"
sidebar_label: "NullCoalesceConverter<T>"
description: "Class NullCoalesceConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NullCoalesceConverter\<T\> {#Aspid_MVVM_StarterKit_NullCoalesceConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Substitutes an authored value for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Object", Name = "Null Coalesce", Tooltip = "Substitutes an authored value for a null one")]
public class NullCoalesceConverter<T> : IConverter<T?, T?>, IConverter where T : class
```

#### Type Parameters

`T` 

The type of the value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NullCoalesceConverter\<T\>](Aspid.MVVM.StarterKit.NullCoalesceConverter-1.md)

#### Implements

[IConverter\<T?, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A destroyed [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) counts as missing, which plain <code>??</code> would not catch.

## Constructors

### NullCoalesceConverter\(\) {#Aspid_MVVM_StarterKit_NullCoalesceConverter_1__ctor}

```csharp
protected NullCoalesceConverter()
```

### NullCoalesceConverter\(T\) {#Aspid_MVVM_StarterKit_NullCoalesceConverter_1__ctor__0_}

```csharp
public NullCoalesceConverter(T fallback)
```

#### Parameters

`fallback` T

Returned when the bound value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">fallback</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed [`Object`](https://docs.unity3d.com/ScriptReference/Object.html).

## Methods

### Convert\(T?\) {#Aspid_MVVM_StarterKit_NullCoalesceConverter_1_Convert__0_}

Returns the specified value, or the fallback when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public T? Convert(T? value)
```

#### Parameters

`value` T?

The value to check.

#### Returns

 T?

The value, or the fallback. A missing or destroyed fallback is reported and still returned.

