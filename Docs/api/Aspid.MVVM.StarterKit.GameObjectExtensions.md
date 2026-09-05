---
title: "Class GameObjectExtensions"
sidebar_label: "GameObjectExtensions"
description: "Class GameObjectExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class GameObjectExtensions {#Aspid_MVVM_StarterKit_GameObjectExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods for [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html) used by the game object binders.

```csharp
public static class GameObjectExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameObjectExtensions](Aspid.MVVM.StarterKit.GameObjectExtensions.md)



## Methods

### SetLayer\(GameObject, int, IBinder\) {#Aspid_MVVM_StarterKit_GameObjectExtensions_SetLayer_UnityEngine_GameObject_System_Int32_Aspid_MVVM_IBinder_}

Sets [`layer`](https://docs.unity3d.com/ScriptReference/GameObject-layer.html) when <code class="paramref">layer</code> is a valid index; otherwise reports it.

```csharp
public static void SetLayer(this GameObject gameObject, int layer, IBinder binder)
```

#### Parameters

`gameObject` GameObject

The object whose layer is set.

`layer` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The layer index, 0 to 31.

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder writing the layer; named in the diagnostic.

