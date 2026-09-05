---
title: "Class FuncConverter<TFrom, TTo>"
sidebar_label: "FuncConverter<TFrom, TTo>"
description: "Class FuncConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class FuncConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_FuncConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a function, or another converter's <code>Convert</code>, as an [`IConverter<T1, T2>`](Aspid.MVVM.StarterKit.IConverter-2.md).

```csharp
[TypeSelectorDisplay(Hidden = true)]
public class FuncConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>, IConverter
```

#### Type Parameters

`TFrom` 

The type of the input value.

`TTo` 

The type of the converted output value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FuncConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.FuncConverter-2.md)

#### Implements

[IConverter\<TFrom?, TTo?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### FuncConverter\(IConverter\<TFrom?, TTo?\>\) {#Aspid_MVVM_StarterKit_FuncConverter_2__ctor_Aspid_MVVM_StarterKit_IConverter__0__1__}

```csharp
public FuncConverter(IConverter<TFrom?, TTo?> converter)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TFrom?, TTo?\>

The converter to wrap.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">converter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### FuncConverter\(Func\<TFrom?, TTo?\>\) {#Aspid_MVVM_StarterKit_FuncConverter_2__ctor_System_Func__0__1__}

```csharp
public FuncConverter(Func<TFrom?, TTo?> converter)
```

#### Parameters

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom?, TTo?\>

The conversion function.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">converter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(TFrom?\) {#Aspid_MVVM_StarterKit_FuncConverter_2_Convert__0_}

Converts the value using the wrapped function.

```csharp
public TTo? Convert(TFrom? value)
```

#### Parameters

`value` TFrom?

The value to convert.

#### Returns

 TTo?

The converted value.

