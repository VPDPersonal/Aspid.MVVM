---
title: "Class BoundsToRectConverter"
sidebar_label: "BoundsToRectConverter"
description: "Class BoundsToRectConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoundsToRectConverter {#Aspid_MVVM_StarterKit_BoundsToRectConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Flattens a bounding box onto a plane.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Bounds/To Rect", Name = "Bounds To Rect", Tooltip = "Flattens a bounding box onto a plane")]
public sealed class BoundsToRectConverter : IConverter<Bounds, Rect>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BoundsToRectConverter](Aspid.MVVM.StarterKit.BoundsToRectConverter.md)

#### Implements

[IConverter\<Bounds, Rect\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### BoundsToRectConverter\(\) {#Aspid_MVVM_StarterKit_BoundsToRectConverter__ctor}

```csharp
public BoundsToRectConverter()
```

#### Remarks

Default: flattening onto XY.

### BoundsToRectConverter\(BoundsPlane\) {#Aspid_MVVM_StarterKit_BoundsToRectConverter__ctor_Aspid_MVVM_StarterKit_BoundsPlane_}

```csharp
public BoundsToRectConverter(BoundsPlane plane)
```

#### Parameters

`plane` [BoundsPlane](Aspid.MVVM.StarterKit.BoundsPlane.md)

Which two axes the box is flattened onto.

## Methods

### Convert\(Bounds\) {#Aspid_MVVM_StarterKit_BoundsToRectConverter_Convert_UnityEngine_Bounds_}

Flattens the specified box.

```csharp
public Rect Convert(Bounds value)
```

#### Parameters

`value` Bounds

The box to flatten.

#### Returns

 Rect

The rectangle, positioned at the box's lower corner on the chosen plane. A plane that is
not a declared [`BoundsPlane`](Aspid.MVVM.StarterKit.BoundsPlane.md) is reported and read as
[`BoundsPlane.XY`](Aspid.MVVM.StarterKit.BoundsPlane.md).

