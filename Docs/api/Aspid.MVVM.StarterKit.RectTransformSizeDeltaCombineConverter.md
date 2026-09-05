---
title: "Class RectTransformSizeDeltaCombineConverter"
sidebar_label: "RectTransformSizeDeltaCombineConverter"
description: "Class RectTransformSizeDeltaCombineConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RectTransformSizeDeltaCombineConverter {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaCombineConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html)'s size delta.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/Combine", Name = "Rect Transform Size Delta", Tooltip = "Combines a 2D vector with a rect transform's size delta")]
public sealed class RectTransformSizeDeltaCombineConverter : Vector2CombineConverter, IConverter<Vector2, Vector2>, IConverter<Vector3, Vector2>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) ← 
[RectTransformSizeDeltaCombineConverter](Aspid.MVVM.StarterKit.RectTransformSizeDeltaCombineConverter.md)

#### Implements

[IConverter\<Vector2, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector3, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A size delta is the difference from the size the anchors give, so it is the element's own
size only while the anchors sit on a point.

## Properties

### Target {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaCombineConverter_Target}

Gets the scene component [`Vector2CombineConverter.VectorTo`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md#Aspid_MVVM_StarterKit_Vector2CombineConverter_VectorTo) is read from. Derived classes must provide
this value so an unassigned or destroyed Inspector reference can be detected before use.

```csharp
protected override Component? Target { get; }
```

#### Property Value

 Component?

### VectorTo {#Aspid_MVVM_StarterKit_RectTransformSizeDeltaCombineConverter_VectorTo}

Gets the reference vector to combine with, which is the rect transform's
[`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html).

```csharp
protected override Vector2 VectorTo { get; }
```

#### Property Value

 Vector2

