---
title: "Class LayoutGroupExtensions"
sidebar_label: "LayoutGroupExtensions"
description: "Class LayoutGroupExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LayoutGroupExtensions {#Aspid_MVVM_StarterKit_LayoutGroupExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods for [`LayoutGroup`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup.html) used by the layout group binders.

```csharp
public static class LayoutGroupExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LayoutGroupExtensions](Aspid.MVVM.StarterKit.LayoutGroupExtensions.md)



## Methods

### SetPadding\(LayoutGroup, RectOffset, RectSides\) {#Aspid_MVVM_StarterKit_LayoutGroupExtensions_SetPadding_UnityEngine_UI_LayoutGroup_UnityEngine_RectOffset_Aspid_MVVM_StarterKit_RectSides_}

Copies the selected <code class="paramref">sides</code> of <code class="paramref">value</code> into
[`padding`](https://docs.unity3d.com/ScriptReference/UI-LayoutGroup-padding.html) and marks the layout for rebuild.

```csharp
public static void SetPadding(this LayoutGroup layout, RectOffset value, RectSides sides)
```

#### Parameters

`layout` LayoutGroup

The layout group whose padding is set.

`value` RectOffset

The padding to copy from.

`sides` [RectSides](Aspid.MVVM.StarterKit.RectSides.md)

The sides to copy.

