---
title: "Class TransformGettersAndSetters"
sidebar_label: "TransformGettersAndSetters"
description: "Class TransformGettersAndSetters — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TransformGettersAndSetters {#Aspid_MVVM_StarterKit_TransformGettersAndSetters}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Provides extension methods for getting and setting position, rotation, and euler angles on a [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html)
in either world or local space.

```csharp
public static class TransformGettersAndSetters
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TransformGettersAndSetters](Aspid.MVVM.StarterKit.TransformGettersAndSetters.md)



## Methods

### GetEulerAngles\(Transform, Space\) {#Aspid_MVVM_StarterKit_TransformGettersAndSetters_GetEulerAngles_UnityEngine_Transform_UnityEngine_Space_}

Gets the euler angles of the [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) in the specified [`Space`](https://docs.unity3d.com/ScriptReference/Space.html).

```csharp
public static Vector3 GetEulerAngles(this Transform transform, Space space)
```

#### Parameters

`transform` Transform

The [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) to read from.

`space` Space

The coordinate space: [`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) returns [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html), [`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) returns [`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

#### Returns

 Vector3

The euler angles in the specified space.

### GetPosition\(Transform, Space\) {#Aspid_MVVM_StarterKit_TransformGettersAndSetters_GetPosition_UnityEngine_Transform_UnityEngine_Space_}

Gets the position of the [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) in the specified [`Space`](https://docs.unity3d.com/ScriptReference/Space.html).

```csharp
public static Vector3 GetPosition(this Transform transform, Space space)
```

#### Parameters

`transform` Transform

The [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) to read from.

`space` Space

The coordinate space: [`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) returns [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html), [`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) returns [`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

#### Returns

 Vector3

The position in the specified space.

### GetRotation\(Transform, Space\) {#Aspid_MVVM_StarterKit_TransformGettersAndSetters_GetRotation_UnityEngine_Transform_UnityEngine_Space_}

Gets the rotation of the [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) in the specified [`Space`](https://docs.unity3d.com/ScriptReference/Space.html).

```csharp
public static Quaternion GetRotation(this Transform transform, Space space)
```

#### Parameters

`transform` Transform

The [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) to read from.

`space` Space

The coordinate space: [`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) returns [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html), [`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) returns [`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

#### Returns

 Quaternion

The rotation in the specified space.

### SetEulerAngles\(Transform, Vector3, Space\) {#Aspid_MVVM_StarterKit_TransformGettersAndSetters_SetEulerAngles_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Space_}

Sets the euler angles of the [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) in the specified [`Space`](https://docs.unity3d.com/ScriptReference/Space.html).

```csharp
public static void SetEulerAngles(this Transform transform, Vector3 value, Space space)
```

#### Parameters

`transform` Transform

The [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) to update.

`value` Vector3

The euler angles to apply.

`space` Space

The coordinate space: [`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) sets [`eulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html), [`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) sets [`localEulerAngles`](https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html).

### SetPosition\(Transform, Vector3, Space\) {#Aspid_MVVM_StarterKit_TransformGettersAndSetters_SetPosition_UnityEngine_Transform_UnityEngine_Vector3_UnityEngine_Space_}

Sets the position of the [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) in the specified [`Space`](https://docs.unity3d.com/ScriptReference/Space.html).

```csharp
public static void SetPosition(this Transform transform, Vector3 value, Space space)
```

#### Parameters

`transform` Transform

The [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) to update.

`value` Vector3

The position to apply.

`space` Space

The coordinate space: [`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) sets [`position`](https://docs.unity3d.com/ScriptReference/Transform-position.html), [`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) sets [`localPosition`](https://docs.unity3d.com/ScriptReference/Transform-localPosition.html).

### SetRotation\(Transform, Quaternion, Space\) {#Aspid_MVVM_StarterKit_TransformGettersAndSetters_SetRotation_UnityEngine_Transform_UnityEngine_Quaternion_UnityEngine_Space_}

Sets the rotation of the [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) in the specified [`Space`](https://docs.unity3d.com/ScriptReference/Space.html).

```csharp
public static void SetRotation(this Transform transform, Quaternion value, Space space)
```

#### Parameters

`transform` Transform

The [`Transform`](https://docs.unity3d.com/ScriptReference/Transform.html) to update.

`value` Quaternion

The rotation to apply.

`space` Space

The coordinate space: [`World`](https://docs.unity3d.com/ScriptReference/Space-World.html) sets [`rotation`](https://docs.unity3d.com/ScriptReference/Transform-rotation.html), [`Self`](https://docs.unity3d.com/ScriptReference/Space-Self.html) sets [`localRotation`](https://docs.unity3d.com/ScriptReference/Transform-localRotation.html).

