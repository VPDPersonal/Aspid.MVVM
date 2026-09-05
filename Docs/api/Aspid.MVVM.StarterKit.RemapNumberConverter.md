---
title: "Class RemapNumberConverter"
sidebar_label: "RemapNumberConverter"
description: "Class RemapNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RemapNumberConverter {#Aspid_MVVM_StarterKit_RemapNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Maps a number from one range onto another.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Remap", Tooltip = "Maps a number from one range onto another")]
public sealed class RemapNumberConverter : TwoWayNumberConverter, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, ITwoWayConverter<int, int>, IConverter<int, int>, ITwoWayConverter<long, long>, IConverter<long, long>, ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md) ← 
[RemapNumberConverter](Aspid.MVVM.StarterKit.RemapNumberConverter.md)

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

### RemapNumberConverter\(\) {#Aspid_MVVM_StarterKit_RemapNumberConverter__ctor}

```csharp
public RemapNumberConverter()
```

#### Remarks

Default: mapping 0..1 onto 0..1.

### RemapNumberConverter\(float, float, float, float, bool\) {#Aspid_MVVM_StarterKit_RemapNumberConverter__ctor_System_Single_System_Single_System_Single_System_Single_System_Boolean_}

```csharp
public RemapNumberConverter(float fromMin, float fromMax, float toMin, float toMax, bool clamp = true)
```

#### Parameters

`fromMin` [float](https://learn.microsoft.com/dotnet/api/system.single)

The low end of the incoming range.

`fromMax` [float](https://learn.microsoft.com/dotnet/api/system.single)

The high end of the incoming range.

`toMin` [float](https://learn.microsoft.com/dotnet/api/system.single)

The low end of the outgoing range.

`toMax` [float](https://learn.microsoft.com/dotnet/api/system.single)

The high end of the outgoing range.

`clamp` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, holds the result inside the outgoing range.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_RemapNumberConverter_Apply_System_Double_}

Maps the number from the incoming range onto the outgoing one.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to map.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The mapped number. A degenerate incoming range yields the outgoing low end.

### Undo\(double\) {#Aspid_MVVM_StarterKit_RemapNumberConverter_Undo_System_Double_}

Maps the number back from the outgoing range onto the incoming one.

```csharp
protected override double Undo(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to map back.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number in the incoming range. A degenerate outgoing range yields its low end.

