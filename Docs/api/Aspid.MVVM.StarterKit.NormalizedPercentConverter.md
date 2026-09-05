---
title: "Class NormalizedPercentConverter"
sidebar_label: "NormalizedPercentConverter"
description: "Class NormalizedPercentConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NormalizedPercentConverter {#Aspid_MVVM_StarterKit_NormalizedPercentConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a 0..1 fraction to a percentage, or the other way round.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Normalized To Percent", Tooltip = "Converts a 0..1 fraction to a percentage, or the other way round")]
public sealed class NormalizedPercentConverter : ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NormalizedPercentConverter](Aspid.MVVM.StarterKit.NormalizedPercentConverter.md)

#### Implements

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

## Remarks

Rounding breaks the round trip: 0.735 goes out as 74 and comes back as 0.74.

## Constructors

### NormalizedPercentConverter\(\) {#Aspid_MVVM_StarterKit_NormalizedPercentConverter__ctor}

```csharp
public NormalizedPercentConverter()
```

#### Remarks

Default: fraction to percent, keeping the fractional percent.

### NormalizedPercentConverter\(bool, bool\) {#Aspid_MVVM_StarterKit_NormalizedPercentConverter__ctor_System_Boolean_System_Boolean_}

```csharp
public NormalizedPercentConverter(bool round, bool isInvert = false)
```

#### Parameters

`round` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, rounds the percentage to a whole number. Breaks the round trip.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, converts a percentage to a fraction instead.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_NormalizedPercentConverter_Convert_System_Single_}

Converts the specified value in the authored direction.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 fraction, or the percentage when inverted.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The percentage, or the 0..1 fraction when inverted. Not clamped.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_NormalizedPercentConverter_ConvertBack_System_Single_}

Converts a value back in the opposite direction.

```csharp
public float ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The percentage, or the 0..1 fraction when inverted.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 fraction, or the percentage when inverted. Not clamped.

