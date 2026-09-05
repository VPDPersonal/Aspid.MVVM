---
title: "Class CapsuleColliderCenterCombineConverter"
sidebar_label: "CapsuleColliderCenterCombineConverter"
description: "Class CapsuleColliderCenterCombineConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CapsuleColliderCenterCombineConverter {#Aspid_MVVM_StarterKit_CapsuleColliderCenterCombineConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`Vector3CombineConverter`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) that reads the reference vector from a
[`CapsuleCollider`](https://docs.unity3d.com/ScriptReference/CapsuleCollider.html)'s center.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/Combine", Name = "Capsule Collider Center", Tooltip = "Combines a vector with a capsule collider's center point")]
public sealed class CapsuleColliderCenterCombineConverter : Vector3CombineConverter, IConverter<Vector3, Vector3>, IConverter<Vector2, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector3CombineConverter](Aspid.MVVM.StarterKit.Vector3CombineConverter.md) ← 
[CapsuleColliderCenterCombineConverter](Aspid.MVVM.StarterKit.CapsuleColliderCenterCombineConverter.md)

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

The center is the only vector a capsule collider exposes, its height and radius are single
floats, and which axis the capsule runs along is chosen by
[`direction`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-direction.html), not by the mode configured here.

## Properties

### Target {#Aspid_MVVM_StarterKit_CapsuleColliderCenterCombineConverter_Target}

Gets the scene component [`Vector3CombineConverter.VectorTo`](Aspid.MVVM.StarterKit.Vector3CombineConverter.md#Aspid_MVVM_StarterKit_Vector3CombineConverter_VectorTo) is read from. Derived classes must provide
this value so an unassigned or destroyed Inspector reference can be detected before use.

```csharp
protected override Component? Target { get; }
```

#### Property Value

 Component?

### VectorTo {#Aspid_MVVM_StarterKit_CapsuleColliderCenterCombineConverter_VectorTo}

Gets the reference vector to combine with, which is the collider's [`center`](https://docs.unity3d.com/ScriptReference/CapsuleCollider-center.html).

```csharp
protected override Vector3 VectorTo { get; }
```

#### Property Value

 Vector3

