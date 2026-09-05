---
title: "Class SequenceConverter<T>"
sidebar_label: "SequenceConverter<T>"
description: "Class SequenceConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SequenceConverter\<T\> {#Aspid_MVVM_StarterKit_SequenceConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies multiple converters to a value in sequence.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Sequence", Tooltip = "Applies multiple converters to a value in sequence")]
public class SequenceConverter<T> : ITwoWayConverter<T?, T?>, IConverter<T?, T?>, IConverter
```

#### Type Parameters

`T` 

The type of the value passing through the chain.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SequenceConverter\<T\>](Aspid.MVVM.StarterKit.SequenceConverter-1.md)

#### Implements

[ITwoWayConverter\<T?, T?\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<T?, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Empty slots in the chain are skipped, not treated as an error.

## Constructors

### SequenceConverter\(\) {#Aspid_MVVM_StarterKit_SequenceConverter_1__ctor}

```csharp
public SequenceConverter()
```

#### Remarks

Default: an empty chain, the value passes through.

### SequenceConverter\(params IConverter\<T?, T?\>\[\]?\) {#Aspid_MVVM_StarterKit_SequenceConverter_1__ctor_Aspid_MVVM_StarterKit_IConverter__0__0____}

```csharp
public SequenceConverter(params IConverter<T?, T?>[]? converters)
```

#### Parameters

`converters` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>\[\]?

The converters to apply in sequence. Empty slots are skipped. The array is copied.

### SequenceConverter\(ConverterFallback\<T?\>?, params IConverter\<T?, T?\>\[\]?\) {#Aspid_MVVM_StarterKit_SequenceConverter_1__ctor_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback__0___Aspid_MVVM_StarterKit_IConverter__0__0____}

```csharp
public SequenceConverter(ConverterFallback<T?>? convertBackFallback, params IConverter<T?, T?>[]? converters)
```

#### Parameters

`convertBackFallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<T?\>?

Returned from [`SequenceConverter<T>.ConvertBack`](Aspid.MVVM.StarterKit.SequenceConverter-1.md#Aspid_MVVM_StarterKit_SequenceConverter_1_ConvertBack__0_) when a link in the chain converts one way only.
When omitted, returns the input value unchanged.

`converters` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, T?\>\[\]?

The converters to apply in sequence. Empty slots are skipped. The array is copied.

## Methods

### Convert\(T?\) {#Aspid_MVVM_StarterKit_SequenceConverter_1_Convert__0_}

Applies each converter in order.

```csharp
public T? Convert(T? value)
```

#### Parameters

`value` T?

The value to convert.

#### Returns

 T?

The value after the last converter.

### ConvertBack\(T?\) {#Aspid_MVVM_StarterKit_SequenceConverter_1_ConvertBack__0_}

Undoes each converter in reverse order.

```csharp
public T? ConvertBack(T? value)
```

#### Parameters

`value` T?

The value to convert back.

#### Returns

 T?

The value with every link undone, or the fallback if any link converts one way only.

#### Remarks

A one-way link is reported and nothing is undone.

