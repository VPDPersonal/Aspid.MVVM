---
title: "Class SecondsToTimeStringConverter"
sidebar_label: "SecondsToTimeStringConverter"
description: "Class SecondsToTimeStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SecondsToTimeStringConverter {#Aspid_MVVM_StarterKit_SecondsToTimeStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes a number of seconds as a clock reading.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Seconds To Time", Tooltip = "Writes a number of seconds as a clock reading")]
public sealed class SecondsToTimeStringConverter : IConverter<float, string>, IConverter<double, string>, IConverter<int, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SecondsToTimeStringConverter](Aspid.MVVM.StarterKit.SecondsToTimeStringConverter.md)

#### Implements

[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A countdown usually wants [`RoundMode.Ceil`](Aspid.MVVM.StarterKit.RoundMode.md), a stopwatch [`RoundMode.Floor`](Aspid.MVVM.StarterKit.RoundMode.md).

## Constructors

### SecondsToTimeStringConverter\(\) {#Aspid_MVVM_StarterKit_SecondsToTimeStringConverter__ctor}

```csharp
public SecondsToTimeStringConverter()
```

#### Remarks

Default: writing mm:ss.

### SecondsToTimeStringConverter\(TimeLayout, RoundMode, bool\) {#Aspid_MVVM_StarterKit_SecondsToTimeStringConverter__ctor_Aspid_MVVM_StarterKit_TimeLayout_Aspid_MVVM_StarterKit_RoundMode_System_Boolean_}

```csharp
public SecondsToTimeStringConverter(TimeLayout layout, RoundMode rounding = RoundMode.Ceil, bool padLeading = true)
```

#### Parameters

`layout` [TimeLayout](Aspid.MVVM.StarterKit.TimeLayout.md)

Which units to show.

`rounding` [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

How to drop the fractional second.

`padLeading` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, pads the leading unit to two digits.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_SecondsToTimeStringConverter_Convert_System_Single_}

Converts the specified value.

```csharp
public string Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

### Convert\(double\) {#Aspid_MVVM_StarterKit_SecondsToTimeStringConverter_Convert_System_Double_}

Converts the specified value.

```csharp
public string Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

### Convert\(int\) {#Aspid_MVVM_StarterKit_SecondsToTimeStringConverter_Convert_System_Int32_}

Converts the specified value.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The converted value.

