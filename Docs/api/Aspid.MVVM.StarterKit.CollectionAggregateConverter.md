---
title: "Class CollectionAggregateConverter"
sidebar_label: "CollectionAggregateConverter"
description: "Class CollectionAggregateConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionAggregateConverter {#Aspid_MVVM_StarterKit_CollectionAggregateConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reduces a collection of numbers to one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To Number", Name = "Aggregate", Tooltip = "Reduces a collection of numbers to one")]
public sealed class CollectionAggregateConverter : IConverter<IEnumerable<int>?, int>, IConverter<IEnumerable<int>?, long>, IConverter<IEnumerable<int>?, float>, IConverter<IEnumerable<int>?, double>, IConverter<IEnumerable<long>?, int>, IConverter<IEnumerable<long>?, long>, IConverter<IEnumerable<long>?, float>, IConverter<IEnumerable<long>?, double>, IConverter<IEnumerable<float>?, int>, IConverter<IEnumerable<float>?, long>, IConverter<IEnumerable<float>?, float>, IConverter<IEnumerable<float>?, double>, IConverter<IEnumerable<double>?, int>, IConverter<IEnumerable<double>?, long>, IConverter<IEnumerable<double>?, float>, IConverter<IEnumerable<double>?, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionAggregateConverter](Aspid.MVVM.StarterKit.CollectionAggregateConverter.md)

#### Implements

[IConverter\<IEnumerable\<int\>?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<int\>?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<int\>?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<int\>?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<long\>?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<long\>?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<long\>?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<long\>?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<float\>?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<float\>?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<float\>?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<float\>?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<double\>?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<double\>?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<double\>?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<double\>?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Computed in [`Double`](https://learn.microsoft.com/dotnet/api/system.double): int and long results truncate and saturate, long values past 2^53 lose precision.

## Constructors

### CollectionAggregateConverter\(\) {#Aspid_MVVM_StarterKit_CollectionAggregateConverter__ctor}

```csharp
public CollectionAggregateConverter()
```

#### Remarks

Default: computing a sum.

### CollectionAggregateConverter\(AggregateOperation, double\) {#Aspid_MVVM_StarterKit_CollectionAggregateConverter__ctor_Aspid_MVVM_StarterKit_AggregateOperation_System_Double_}

```csharp
public CollectionAggregateConverter(AggregateOperation operation, double emptyResult = 0)
```

#### Parameters

`operation` [AggregateOperation](Aspid.MVVM.StarterKit.AggregateOperation.md)

What to compute.

`emptyResult` [double](https://learn.microsoft.com/dotnet/api/system.double)

Returned for an empty collection.

## Methods

### Reduce\(IEnumerable\<double\>?\) {#Aspid_MVVM_StarterKit_CollectionAggregateConverter_Reduce_System_Collections_Generic_IEnumerable_System_Double__}

Reduces the specified collection.

```csharp
public double Reduce(IEnumerable<double>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>?

The numbers to reduce.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, always in [`Double`](https://learn.microsoft.com/dotnet/api/system.double), or the empty result when there is nothing to
reduce or the operation is not a declared [`AggregateOperation`](Aspid.MVVM.StarterKit.AggregateOperation.md).

### Reduce\(IEnumerable\<int\>?\) {#Aspid_MVVM_StarterKit_CollectionAggregateConverter_Reduce_System_Collections_Generic_IEnumerable_System_Int32__}

Reduces the specified collection.

```csharp
public double Reduce(IEnumerable<int>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

The numbers to reduce.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, always in [`Double`](https://learn.microsoft.com/dotnet/api/system.double), or the empty result when there is nothing to
reduce or the operation is not a declared [`AggregateOperation`](Aspid.MVVM.StarterKit.AggregateOperation.md).

### Reduce\(IEnumerable\<long\>?\) {#Aspid_MVVM_StarterKit_CollectionAggregateConverter_Reduce_System_Collections_Generic_IEnumerable_System_Int64__}

Reduces the specified collection.

```csharp
public double Reduce(IEnumerable<long>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[long](https://learn.microsoft.com/dotnet/api/system.int64)\>?

The numbers to reduce.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, always in [`Double`](https://learn.microsoft.com/dotnet/api/system.double), or the empty result when there is nothing to
reduce or the operation is not a declared [`AggregateOperation`](Aspid.MVVM.StarterKit.AggregateOperation.md).

### Reduce\(IEnumerable\<float\>?\) {#Aspid_MVVM_StarterKit_CollectionAggregateConverter_Reduce_System_Collections_Generic_IEnumerable_System_Single__}

Reduces the specified collection.

```csharp
public double Reduce(IEnumerable<float>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[float](https://learn.microsoft.com/dotnet/api/system.single)\>?

The numbers to reduce.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, always in [`Double`](https://learn.microsoft.com/dotnet/api/system.double), or the empty result when there is nothing to
reduce or the operation is not a declared [`AggregateOperation`](Aspid.MVVM.StarterKit.AggregateOperation.md).

