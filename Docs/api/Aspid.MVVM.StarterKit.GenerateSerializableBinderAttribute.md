---
title: "Class GenerateSerializableBinderAttribute"
sidebar_label: "GenerateSerializableBinderAttribute"
description: "Class GenerateSerializableBinderAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class GenerateSerializableBinderAttribute {#Aspid_MVVM_StarterKit_GenerateSerializableBinderAttribute}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Declares that the serializable half of this binder family is generated from the MonoBehaviour half it is
applied to.

```csharp
[AttributeUsage(AttributeTargets.Class)]
public sealed class GenerateSerializableBinderAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[GenerateSerializableBinderAttribute](Aspid.MVVM.StarterKit.GenerateSerializableBinderAttribute.md)



## Examples


```csharp
[GenerateSerializableBinder]
public class CameraFieldOfViewMonoBinder : ComponentFloatMonoBinder<Camera> { … }
```


## Remarks

Emits <code>\{Name\}Binder</code> over the matching <code>Target*Binder</code>, carrying the body, the serialized options and the
documentation across and synthesising the constructor from the options. A twin that already exists by name is skipped.
The MonoBehaviour half stays hand-written: Unity needs a MonoScript asset, which only a file of its own provides.

