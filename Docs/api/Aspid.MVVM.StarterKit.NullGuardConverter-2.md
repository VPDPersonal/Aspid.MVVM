---
title: "Class NullGuardConverter<TFrom, TTo>"
sidebar_label: "NullGuardConverter<TFrom, TTo>"
description: "Class NullGuardConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NullGuardConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_NullGuardConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Substitutes a fixed result for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> input instead of passing it on.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Null Guard", Tooltip = "Substitutes a fixed result for a null input instead of passing it on")]
public class NullGuardConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NullGuardConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.NullGuardConverter-2.md)

#### Implements

[IConverter\<TFrom?, TTo?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Settles what a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> input means regardless of how the inner converter treats it.

## Constructors

### NullGuardConverter\(\) {#Aspid_MVVM_StarterKit_NullGuardConverter_2__ctor}

```csharp
protected NullGuardConverter()
```

### NullGuardConverter\(IConverter\<TFrom?, TTo?\>, TTo?\) {#Aspid_MVVM_StarterKit_NullGuardConverter_2__ctor_Aspid_MVVM_StarterKit_IConverter__0__1___1_}

```csharp
public NullGuardConverter(IConverter<TFrom?, TTo?> inner, TTo? nullResult = default)
```

#### Parameters

`inner` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TFrom?, TTo?\>

The converter to run for a non-null value.

`nullResult` TTo?

Returned when the incoming value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">inner</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_NullGuardConverter_2_Convert__0_}

Converts the specified value, short-circuiting <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The converted value, or the null result when the input is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or the inner converter is missing.

