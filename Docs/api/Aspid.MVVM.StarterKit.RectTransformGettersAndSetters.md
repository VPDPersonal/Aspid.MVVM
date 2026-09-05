---
title: "Class RectTransformGettersAndSetters"
sidebar_label: "RectTransformGettersAndSetters"
description: "Class RectTransformGettersAndSetters — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RectTransformGettersAndSetters {#Aspid_MVVM_StarterKit_RectTransformGettersAndSetters}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods that read and write the anchored position of a [`RectTransform`](https://docs.unity3d.com/ScriptReference/RectTransform.html) by [`Space`](https://docs.unity3d.com/ScriptReference/Space.html)
and its size delta by [`SizeDeltaMode`](Aspid.MVVM.StarterKit.SizeDeltaMode.md).

```csharp
public static class RectTransformGettersAndSetters
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RectTransformGettersAndSetters](Aspid.MVVM.StarterKit.RectTransformGettersAndSetters.md)



## Methods

### GetAnchoredPosition\(RectTransform, Space\) {#Aspid_MVVM_StarterKit_RectTransformGettersAndSetters_GetAnchoredPosition_UnityEngine_RectTransform_UnityEngine_Space_}

Gets the anchored position in the specified space.

```csharp
public static Vector3 GetAnchoredPosition(this RectTransform transform, Space space)
```

#### Parameters

`transform` RectTransform

The rect transform to read.

`space` Space

[`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) reads [`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html),
[`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) reads [`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

#### Returns

 Vector3

The anchored position.

### SetAnchoredPosition\(RectTransform, Vector3, Space\) {#Aspid_MVVM_StarterKit_RectTransformGettersAndSetters_SetAnchoredPosition_UnityEngine_RectTransform_UnityEngine_Vector3_UnityEngine_Space_}

Sets the anchored position in the specified space.

```csharp
public static void SetAnchoredPosition(this RectTransform transform, Vector3 value, Space space)
```

#### Parameters

`transform` RectTransform

The rect transform to write.

`value` Vector3

The anchored position to apply.

`space` Space

[`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) writes [`anchoredPosition`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition.html),
[`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) writes [`anchoredPosition3D`](https://docs.unity3d.com/ScriptReference/RectTransform-anchoredPosition3D.html).

### SetSizeDelta\(RectTransform, Vector3, SizeDeltaMode\) {#Aspid_MVVM_StarterKit_RectTransformGettersAndSetters_SetSizeDelta_UnityEngine_RectTransform_UnityEngine_Vector3_Aspid_MVVM_StarterKit_SizeDeltaMode_}

Sets [`sizeDelta`](https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html): <code>x</code> as the width and <code>y</code> as the height, on the axes <code class="paramref">mode</code> selects.
A non-finite size is reported and skipped.

```csharp
public static void SetSizeDelta(this RectTransform transform, Vector3 value, SizeDeltaMode mode)
```

#### Parameters

`transform` RectTransform

The rect transform to write.

`value` Vector3

The size to apply.

`mode` [SizeDeltaMode](Aspid.MVVM.StarterKit.SizeDeltaMode.md)

Which axes are written.

