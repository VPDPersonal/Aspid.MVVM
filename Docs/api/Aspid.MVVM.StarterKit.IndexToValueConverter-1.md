---
title: "Class IndexToValueConverter<T>"
sidebar_label: "IndexToValueConverter<T>"
description: "Class IndexToValueConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class IndexToValueConverter\<T\> {#Aspid_MVVM_StarterKit_IndexToValueConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Picks a value out of an authored array by index.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Value", Name = "Index To Value", Tooltip = "Picks a value out of an authored array by index")]
public class IndexToValueConverter<T> : IConverter<int, T?>, IConverter<long, T?>, IConverter<float, T?>, IConverter<double, T?>, IConverter
```

#### Type Parameters

`T` 

The type of the values in the array.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[IndexToValueConverter\<T\>](Aspid.MVVM.StarterKit.IndexToValueConverter-1.md)

#### Implements

[IConverter\<int, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A float or double index drops its fraction; a NaN names no position and is reported.

## Constructors

### IndexToValueConverter\(\) {#Aspid_MVVM_StarterKit_IndexToValueConverter_1__ctor}

```csharp
protected IndexToValueConverter()
```

### IndexToValueConverter\(T\[\]?, IndexOutOfRangeMode, T?\) {#Aspid_MVVM_StarterKit_IndexToValueConverter_1__ctor__0___Aspid_MVVM_StarterKit_IndexOutOfRangeMode__0_}

```csharp
public IndexToValueConverter(T[]? values, IndexOutOfRangeMode mode = IndexOutOfRangeMode.Clamp, T? fallback = default)
```

#### Parameters

`values` T\[\]?

The values to pick from, in order.

`mode` [IndexOutOfRangeMode](Aspid.MVVM.StarterKit.IndexOutOfRangeMode.md)

What to do with an index outside the array.

`fallback` T?

Returned for an out-of-range index under [`IndexOutOfRangeMode.Fallback`](Aspid.MVVM.StarterKit.IndexOutOfRangeMode.md), or for an empty array.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_IndexToValueConverter_1_Convert_System_Int32_}

Picks the value at the specified index.

```csharp
public T? Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index to pick.

#### Returns

 T?

The value at that index, resolved through the mode. An empty array or an undeclared mode falls back.

