---
title: "Class RendererExtensions"
sidebar_label: "RendererExtensions"
description: "Class RendererExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RendererExtensions {#Aspid_MVVM_StarterKit_RendererExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods that write validated values to a [`Renderer`](https://docs.unity3d.com/ScriptReference/Renderer.html).

```csharp
public static class RendererExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RendererExtensions](Aspid.MVVM.StarterKit.RendererExtensions.md)



## Methods

### SetMaterials\(Renderer, IConverter\<Material?, Material?\>?, IReadOnlyCollection\<Material\>?\) {#Aspid_MVVM_StarterKit_RendererExtensions_SetMaterials_UnityEngine_Renderer_Aspid_MVVM_StarterKit_IConverter_UnityEngine_Material_UnityEngine_Material__System_Collections_Generic_IReadOnlyCollection_UnityEngine_Material__}

Sets [`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html), passing each material through <code class="paramref">converter</code>.

```csharp
public static void SetMaterials(this Renderer renderer, IConverter<Material?, Material?>? converter, IReadOnlyCollection<Material>? values)
```

#### Parameters

`renderer` Renderer

The renderer whose materials are set.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<Material?, Material?\>?

The converter applied to each material, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it as-is.

`values` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<Material\>?

The materials to assign, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear.

#### Remarks

[`materials`](https://docs.unity3d.com/ScriptReference/Renderer-materials.html) rejects <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, so a missing or empty collection clears
the array.

