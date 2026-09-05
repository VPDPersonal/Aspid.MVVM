---
title: "Class InverseLerpConverter"
sidebar_label: "InverseLerpConverter"
description: "Class InverseLerpConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InverseLerpConverter {#Aspid_MVVM_StarterKit_InverseLerpConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a value in a range to its 0..1 position within it.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Inverse Lerp", Tooltip = "Converts a value in a range to its 0..1 position within it")]
public sealed class InverseLerpConverter : ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InverseLerpConverter](Aspid.MVVM.StarterKit.InverseLerpConverter.md)

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

### InverseLerpConverter\(\) {#Aspid_MVVM_StarterKit_InverseLerpConverter__ctor}

```csharp
public InverseLerpConverter()
```

#### Remarks

Default: over 0..1.

### InverseLerpConverter\(float, float, bool\) {#Aspid_MVVM_StarterKit_InverseLerpConverter__ctor_System_Single_System_Single_System_Boolean_}

```csharp
public InverseLerpConverter(float min, float max, bool clamp = true)
```

#### Parameters

`min` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value that maps to 0.

`max` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value that maps to 1.

`clamp` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, holds the result inside 0..1.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_InverseLerpConverter_Convert_System_Single_}

Converts the specified value to its position in the range.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to locate.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

Its 0..1 position. A degenerate range yields 0.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_InverseLerpConverter_ConvertBack_System_Single_}

Converts a 0..1 position back to a value in the range.

```csharp
public float ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The position to convert.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The value at that position.

