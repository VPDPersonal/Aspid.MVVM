---
title: "Class AsBinderAttribute"
sidebar_label: "AsBinderAttribute"
description: "Class AsBinderAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AsBinderAttribute {#Aspid_MVVM_AsBinderAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to fields or properties of a type carrying [`ViewAttribute`](Aspid.MVVM.ViewAttribute.md);
directs the Source Generator to emit binding code that wires the member to the supplied [`IBinder`](Aspid.MVVM.IBinder.md) type.

```csharp
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
public sealed class AsBinderAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[AsBinderAttribute](Aspid.MVVM.AsBinderAttribute.md)



## Constructors

### AsBinderAttribute\(Type, params object\[\]\) {#Aspid_MVVM_AsBinderAttribute__ctor_System_Type_System_Object___}

Initializes a new instance of the [`AsBinderAttribute`](Aspid.MVVM.AsBinderAttribute.md) with the specified [`IBinder`](Aspid.MVVM.IBinder.md) type and optional arguments.

```csharp
public AsBinderAttribute(Type type, params object[] arguments)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The type of [`IBinder`](Aspid.MVVM.IBinder.md) that will be used to bind the field or property.

`arguments` [object](https://learn.microsoft.com/dotnet/api/system.object)\[\]

Additional arguments that can be passed to the constructor of the [`IBinder`](Aspid.MVVM.IBinder.md) type.

## Fields

### Type {#Aspid_MVVM_AsBinderAttribute_Type}

Gets the [`IBinder`](Aspid.MVVM.IBinder.md) type used to bind the field or property.

```csharp
public readonly Type Type
```

#### Field Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)

