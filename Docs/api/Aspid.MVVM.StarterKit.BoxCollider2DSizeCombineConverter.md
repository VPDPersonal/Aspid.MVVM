---
title: "Class BoxCollider2DSizeCombineConverter"
sidebar_label: "BoxCollider2DSizeCombineConverter"
description: "Class BoxCollider2DSizeCombineConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoxCollider2DSizeCombineConverter {#Aspid_MVVM_StarterKit_BoxCollider2DSizeCombineConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Vector2CombineConverter`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) that reads the reference vector from a
[`BoxCollider2D`](https://docs.unity3d.com/ScriptReference/BoxCollider2D.html)'s size.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/Combine", Name = "Box Collider 2D Size", Tooltip = "Combines a 2D vector with a 2D box collider's size")]
public sealed class BoxCollider2DSizeCombineConverter : Vector2CombineConverter, IConverter<Vector2, Vector2>, IConverter<Vector3, Vector2>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector2CombineConverter](Aspid.MVVM.StarterKit.Vector2CombineConverter.md) ← 
[BoxCollider2DSizeCombineConverter](Aspid.MVVM.StarterKit.BoxCollider2DSizeCombineConverter.md)

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

A collider size is unscaled: the transform's scale multiplies it afterward.

## Properties

### Target {#Aspid_MVVM_StarterKit_BoxCollider2DSizeCombineConverter_Target}

Gets the scene component [`Vector2CombineConverter.VectorTo`](Aspid.MVVM.StarterKit.Vector2CombineConverter.md#Aspid_MVVM_StarterKit_Vector2CombineConverter_VectorTo) is read from. Derived classes must provide
this value so an unassigned or destroyed Inspector reference can be detected before use.

```csharp
protected override Component? Target { get; }
```

#### Property Value

 Component?

### VectorTo {#Aspid_MVVM_StarterKit_BoxCollider2DSizeCombineConverter_VectorTo}

Gets the reference vector to combine with, which is the collider's [`size`](https://docs.unity3d.com/ScriptReference/BoxCollider2D-size.html).

```csharp
protected override Vector2 VectorTo { get; }
```

#### Property Value

 Vector2

