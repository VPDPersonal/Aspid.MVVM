---
title: "Interface ITwoWayConverter<TFrom, TTo>"
sidebar_label: "ITwoWayConverter<TFrom, TTo>"
description: "Interface ITwoWayConverter<TFrom, TTo> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface ITwoWayConverter\<TFrom, TTo\> {#Aspid_MVVM_StarterKit_ITwoWayConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts values back as well, for the trip from the View to the ViewModel.

```csharp
public interface ITwoWayConverter<TFrom, TTo> : IConverter<TFrom, TTo>, IConverter
```

#### Type Parameters

`TFrom` 

The type held by the ViewModel.

`TTo` 

The type held by the View.

#### Implements

[IConverter\<TFrom, TTo\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)

#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A binder in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) or [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) sends the
value unchanged when the converter does not offer the reverse conversion.

<p>
[`ITwoWayConverter<T1, T2>.ConvertBack`](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md#Aspid_MVVM_StarterKit_ITwoWayConverter_2_ConvertBack__1_) must satisfy <code>ConvertBack(Convert(x)) == x</code>, otherwise the
value drifts on every round trip.
</p>

## Methods

### ConvertBack\(TTo\) {#Aspid_MVVM_StarterKit_ITwoWayConverter_2_ConvertBack__1_}

Converts a value coming back from the View.

```csharp
TFrom ConvertBack(TTo value)
```

#### Parameters

`value` TTo

The value to convert back.

#### Returns

 TFrom

The value as the ViewModel expects it.

