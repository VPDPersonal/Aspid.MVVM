---
title: "Class DegreesRadiansConverter"
sidebar_label: "DegreesRadiansConverter"
description: "Class DegreesRadiansConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DegreesRadiansConverter {#Aspid_MVVM_StarterKit_DegreesRadiansConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts between degrees and radians.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Degrees To Radians", Tooltip = "Converts between degrees and radians")]
public sealed class DegreesRadiansConverter : ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DegreesRadiansConverter](Aspid.MVVM.StarterKit.DegreesRadiansConverter.md)

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

## Constructors

### DegreesRadiansConverter\(\) {#Aspid_MVVM_StarterKit_DegreesRadiansConverter__ctor}

```csharp
public DegreesRadiansConverter()
```

#### Remarks

Default: degrees to radians.

### DegreesRadiansConverter\(bool\) {#Aspid_MVVM_StarterKit_DegreesRadiansConverter__ctor_System_Boolean_}

```csharp
public DegreesRadiansConverter(bool isInvert)
```

#### Parameters

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, converts radians to degrees instead.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_DegreesRadiansConverter_Convert_System_Single_}

Converts the specified angle in the authored direction.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees, or radians when inverted.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in radians, or degrees when inverted.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_DegreesRadiansConverter_ConvertBack_System_Single_}

Converts the specified angle back in the opposite direction.

```csharp
public float ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in radians, or degrees when inverted.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees, or radians when inverted.

