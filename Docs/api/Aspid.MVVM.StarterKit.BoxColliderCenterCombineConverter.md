---
title: "Class BoxColliderCenterCombineConverter"
sidebar_label: "BoxColliderCenterCombineConverter"
description: "Class BoxColliderCenterCombineConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoxColliderCenterCombineConverter {#Aspid_MVVM_StarterKit_BoxColliderCenterCombineConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`BoxCollider`](https://docs.unity3d.com/ScriptReference/BoxCollider.html)'s center.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/Combine", Name = "Box Collider Center", Tooltip = "Combines a vector with a box collider's center point")]
public sealed class BoxColliderCenterCombineConverter : Vector3CombineConverter, IConverter<Vector3, Vector3>, IConverter<Vector2, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) ← 
[BoxColliderCenterCombineConverter](Aspid.MVVM.StarterKit.BoxColliderCenterCombineConverter.md)

#### Implements

[IConverter\<Vector3, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector2, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A collider center is an offset in the object's own space, not a point in the scene.

## Properties

### Target {#Aspid_MVVM_StarterKit_BoxColliderCenterCombineConverter_Target}

Gets the scene component [`Vector3CombineConverter.VectorTo`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md#Aspid_MVVM_StarterKit_Vector3CombineConverter_VectorTo) is read from. Derived classes must provide
this value so an unassigned or destroyed Inspector reference can be detected before use.

```csharp
protected override Component? Target { get; }
```

#### Property Value

 Component?

### VectorTo {#Aspid_MVVM_StarterKit_BoxColliderCenterCombineConverter_VectorTo}

Gets the reference vector to combine with, which is the collider's [`center`](https://docs.unity3d.com/ScriptReference/BoxCollider-center.html).

```csharp
protected override Vector3 VectorTo { get; }
```

#### Property Value

 Vector3

