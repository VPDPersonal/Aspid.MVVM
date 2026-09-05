---
title: "Class UsedInModesAttribute"
sidebar_label: "UsedInModesAttribute"
description: "Class UsedInModesAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class UsedInModesAttribute {#Aspid_MVVM_UsedInModesAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Marks a serialized field as used only under the specified binding modes, so the Inspector
can disable it while the hosting binder is bound in any other.
This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Field)]
public sealed class UsedInModesAttribute : PropertyAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
PropertyAttribute ← 
[UsedInModesAttribute](Aspid.MVVM.UsedInModesAttribute.md)



## Remarks

The field can sit on the binder itself or anywhere inside a serialized object the binder
holds; the nearest binder above it decides. Outside any binder the field stays enabled.

## Constructors

### UsedInModesAttribute\(params BindMode\[\]\) {#Aspid_MVVM_UsedInModesAttribute__ctor_Aspid_MVVM_BindMode___}

```csharp
public UsedInModesAttribute(params BindMode[] modes)
```

#### Parameters

`modes` [BindMode](Aspid.MVVM.BindMode.md)\[\]

The binding modes the field is used under.

## Properties

### Modes {#Aspid_MVVM_UsedInModesAttribute_Modes}

Gets the binding modes the field is used under.

```csharp
public IReadOnlyList<BindMode> Modes { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[BindMode](Aspid.MVVM.BindMode.md)\>

