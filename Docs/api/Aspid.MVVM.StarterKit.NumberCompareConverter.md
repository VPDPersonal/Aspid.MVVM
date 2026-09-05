---
title: "Class NumberCompareConverter"
sidebar_label: "NumberCompareConverter"
description: "Class NumberCompareConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NumberCompareConverter {#Aspid_MVVM_StarterKit_NumberCompareConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts numeric values to boolean based on comparison operations.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Bool", Name = "Compare", Tooltip = "Converts numeric values to boolean based on comparison operations")]
public sealed class NumberCompareConverter : IConverter<int, bool>, IConverter<long, bool>, IConverter<float, bool>, IConverter<double, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberCompareConverter](Aspid.MVVM.StarterKit.NumberCompareConverter.md)

#### Implements

[IConverter\<int, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The tolerance follows the incoming type: none for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) and [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64),
relative 1e-6 for [`Single`](https://learn.microsoft.com/dotnet/api/system.single), relative 1e-12 for [`Double`](https://learn.microsoft.com/dotnet/api/system.double).

## Constructors

### NumberCompareConverter\(\) {#Aspid_MVVM_StarterKit_NumberCompareConverter__ctor}

```csharp
public NumberCompareConverter()
```

#### Remarks

Default: testing equality with zero.

### NumberCompareConverter\(ComparisonMode, double\) {#Aspid_MVVM_StarterKit_NumberCompareConverter__ctor_Aspid_MVVM_StarterKit_ComparisonMode_System_Double_}

```csharp
public NumberCompareConverter(ComparisonMode comparison, double value)
```

#### Parameters

`comparison` [ComparisonMode](Aspid.MVVM.StarterKit.ComparisonMode.md)

How the bound number is compared with <code class="paramref">value</code>.

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the bound one is compared against.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_NumberCompareConverter_Convert_System_Single_}

Compares the bound number with the authored one.

```csharp
public bool Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result. An undeclared comparison reports an error and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Convert\(double\) {#Aspid_MVVM_StarterKit_NumberCompareConverter_Convert_System_Double_}

Compares the bound number with the authored one.

```csharp
public bool Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result. An undeclared comparison reports an error and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Convert\(int\) {#Aspid_MVVM_StarterKit_NumberCompareConverter_Convert_System_Int32_}

Compares the bound number with the authored one.

```csharp
public bool Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result. An undeclared comparison reports an error and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Convert\(long\) {#Aspid_MVVM_StarterKit_NumberCompareConverter_Convert_System_Int64_}

Compares the bound number with the authored one.

```csharp
public bool Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value to compare.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result. An undeclared comparison reports an error and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Remarks

A long beyond 2^53 loses precision in [`Double`](https://learn.microsoft.com/dotnet/api/system.double).

