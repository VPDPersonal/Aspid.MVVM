---
title: "Class LerpNumberConverter"
sidebar_label: "LerpNumberConverter"
description: "Class LerpNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LerpNumberConverter {#Aspid_MVVM_StarterKit_LerpNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a 0..1 position to a value in a range.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Lerp", Tooltip = "Converts a 0..1 position to a value in a range")]
public sealed class LerpNumberConverter : TwoWayNumberConverter, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, ITwoWayConverter<int, int>, IConverter<int, int>, ITwoWayConverter<long, long>, IConverter<long, long>, ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md) ← 
[LerpNumberConverter](Aspid.MVVM.StarterKit.LerpNumberConverter.md)

#### Implements

[IConverter\<int, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<int, int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<int, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<long, long\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<long, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<float, float\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, double\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### LerpNumberConverter\(\) {#Aspid_MVVM_StarterKit_LerpNumberConverter__ctor}

```csharp
public LerpNumberConverter()
```

#### Remarks

Default: over 0..1.

### LerpNumberConverter\(float, float, bool\) {#Aspid_MVVM_StarterKit_LerpNumberConverter__ctor_System_Single_System_Single_System_Boolean_}

```csharp
public LerpNumberConverter(float from, float to, bool clamp = true)
```

#### Parameters

`from` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value 0 maps to.

`to` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value 1 maps to.

`clamp` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, holds the incoming position inside 0..1.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_LerpNumberConverter_Apply_System_Double_}

Converts the specified position to a value in the range.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The 0..1 position.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The value at that position.

### Undo\(double\) {#Aspid_MVVM_StarterKit_LerpNumberConverter_Undo_System_Double_}

Converts a value in the range back to its position.

```csharp
protected override double Undo(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value to locate.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

Its 0..1 position. A degenerate range yields 0.

