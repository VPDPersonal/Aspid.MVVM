---
title: "Interface IConverter<TFrom, TTo>"
sidebar_label: "IConverter<TFrom, TTo>"
description: "Interface IConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_IConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a value of type <code class="typeparamref">TFrom</code> into a value of type <code class="typeparamref">TTo</code>.

```csharp
public interface IConverter<in TFrom, out TTo> : IConverter
```

#### Type Parameters

`TFrom` 

The type of the value to convert.

`TTo` 

The type of the converted value.

#### Implements

[IConverter](Aspid.MVVM.StarterKit.IConverter.md)

#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

One-directional; the reverse trip needs [`ITwoWayConverter<T1, T2>`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md).
[`IConverter<T1, T2>.Convert`](Aspid.MVVM.StarterKit.IConverter-2.md#Aspid_MVVM_StarterKit_IConverter_2_Convert__0_) runs on every value push, so keep it pure and allocation-free.

## Methods

### Convert\(TFrom\) {#Aspid_MVVM_StarterKit_IConverter_2_Convert__0_}

Converts the specified value.

```csharp
TTo Convert(TFrom value)
```

#### Parameters

`value` TFrom

The value to convert.

#### Returns

 TTo

The converted value.

