---
title: "Class RectOffsetScaleConverter"
sidebar_label: "RectOffsetScaleConverter"
description: "Class RectOffsetScaleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RectOffsetScaleConverter {#Aspid_MVVM_StarterKit_RectOffsetScaleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Scales a padding.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Rect Offset", Name = "Scale", Tooltip = "Scales a padding")]
public sealed class RectOffsetScaleConverter : IConverter<RectOffset, RectOffset>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RectOffsetScaleConverter](Aspid.MVVM.StarterKit.RectOffsetScaleConverter.md)

#### Implements

[IConverter\<RectOffset, RectOffset\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RectOffsetScaleConverter\(\) {#Aspid_MVVM_StarterKit_RectOffsetScaleConverter__ctor}

```csharp
public RectOffsetScaleConverter()
```

#### Remarks

Default: leaving every side as it is.

### RectOffsetScaleConverter\(float, RectSides, RoundMode\) {#Aspid_MVVM_StarterKit_RectOffsetScaleConverter__ctor_System_Single_Aspid_MVVM_StarterKit_RectSides_Aspid_MVVM_StarterKit_RoundMode_}

```csharp
public RectOffsetScaleConverter(float scale, RectSides sides = RectSides.All, RoundMode rounding = RoundMode.Round)
```

#### Parameters

`scale` [float](https://learn.microsoft.com/dotnet/api/system.single)

What the padding is multiplied by.

`sides` [RectSides](Aspid.MVVM.StarterKit.RectSides.md)

Which sides are scaled.

`rounding` [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

Which way to drop the fraction.

## Methods

### Convert\(RectOffset?\) {#Aspid_MVVM_StarterKit_RectOffsetScaleConverter_Convert_UnityEngine_RectOffset_}

Scales the specified padding.

```csharp
public RectOffset Convert(RectOffset? value)
```

#### Parameters

`value` RectOffset?

The padding to scale, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to read no padding at all.

#### Returns

 RectOffset

The scaled padding. The same instance is returned every call, so copy it if it must
outlive the next push. A side too large for a whole number is held at the nearest one. A
rounding that is not a declared [`RoundMode`](Aspid.MVVM.StarterKit.RoundMode.md) value reports an error and the
fraction is truncated.

