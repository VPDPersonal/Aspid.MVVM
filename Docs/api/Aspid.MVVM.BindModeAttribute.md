---
title: "Class BindModeAttribute"
sidebar_label: "BindModeAttribute"
description: "Class BindModeAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindModeAttribute {#Aspid_MVVM_BindModeAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Attribute used to specify allowed binding modes for a property in the Unity Editor.
This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
public sealed class BindModeAttribute : PropertyAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
PropertyAttribute ← 
[BindModeAttribute](Aspid.MVVM.BindModeAttribute.md)



## Constructors

### BindModeAttribute\(params BindMode\[\]\) {#Aspid_MVVM_BindModeAttribute__ctor_Aspid_MVVM_BindMode___}

Initializes a new instance of the [`BindModeAttribute`](Aspid.MVVM.BindModeAttribute.md) class with the specified binding modes.
If no modes are provided and neither [`BindModeAttribute.IsOne`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsOne) nor [`BindModeAttribute.IsTwo`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsTwo) is set, the behavior is equivalent to [`BindModeAttribute.IsAll`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsAll).
If [`BindModeAttribute.IsOne`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsOne) and [`BindModeAttribute.IsTwo`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsTwo) are both <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, the behavior is equivalent to [`BindModeAttribute.IsAll`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsAll).
If [`BindModeAttribute.IsOne`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsOne) or [`BindModeAttribute.IsTwo`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsTwo) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and modes are provided, the allowed modes are a combination of the specified modes and the modes defined by the properties.

```csharp
public BindModeAttribute(params BindMode[] modes)
```

#### Parameters

`modes` [BindMode](Aspid.MVVM.BindMode.md)\[\]

The binding modes that are allowed for the property.

## Properties

### IsAll {#Aspid_MVVM_BindModeAttribute_IsAll}

Indicates whether all binding modes (except [`BindMode.None`](Aspid.MVVM.BindMode.md)) are allowed.
If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, all modes except [`BindMode.None`](Aspid.MVVM.BindMode.md) are enabled.

```csharp
public bool IsAll { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsOne {#Aspid_MVVM_BindModeAttribute_IsOne}

Indicates whether only [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) are allowed.
If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only these two modes are enabled.

```csharp
public bool IsOne { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsTwo {#Aspid_MVVM_BindModeAttribute_IsTwo}

Indicates whether only [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) are allowed.
If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only these two modes are enabled.

```csharp
public bool IsTwo { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Modes {#Aspid_MVVM_BindModeAttribute_Modes}

Gets the array of allowed binding modes for the property.

```csharp
public BindMode[] Modes { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)\[\]

