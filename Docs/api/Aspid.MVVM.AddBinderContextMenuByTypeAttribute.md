---
title: "Class AddBinderContextMenuByTypeAttribute"
sidebar_label: "AddBinderContextMenuByTypeAttribute"
description: "Class AddBinderContextMenuByTypeAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AddBinderContextMenuByTypeAttribute {#Aspid_MVVM_AddBinderContextMenuByTypeAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Editor-only attribute that registers a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) class in the "Add Binder" context menu
based solely on the target component type. Unlike [`AddBinderContextMenuAttribute`](Aspid.MVVM.AddBinderContextMenuAttribute.md),
this attribute does not support property auto-population or custom menu paths.
Can be applied multiple times to associate a binder with several component types.

```csharp
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class AddBinderContextMenuByTypeAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[AddBinderContextMenuByTypeAttribute](Aspid.MVVM.AddBinderContextMenuByTypeAttribute.md)



## Constructors

### AddBinderContextMenuByTypeAttribute\(Type\) {#Aspid_MVVM_AddBinderContextMenuByTypeAttribute__ctor_System_Type_}

Initializes the attribute for the specified component type.

```csharp
public AddBinderContextMenuByTypeAttribute(Type componentType)
```

#### Parameters

`componentType` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The component type whose context menu should include this binder.

## Properties

### Type {#Aspid_MVVM_AddBinderContextMenuByTypeAttribute_Type}

The component type this binder entry is associated with.

```csharp
public Type Type { get; }
```

#### Property Value

 [Type](https://learn.microsoft.com/dotnet/api/system.type)

