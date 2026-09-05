---
title: "Class BindModeOverrideAttribute"
sidebar_label: "BindModeOverrideAttribute"
description: "Class BindModeOverrideAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindModeOverrideAttribute {#Aspid_MVVM_BindModeOverrideAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Attribute used to override allowed binding modes for a class.
This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.

```csharp
[Conditional("DEBUG")]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class BindModeOverrideAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BindModeOverrideAttribute](Aspid.MVVM.BindModeOverrideAttribute.md)



## Constructors

### BindModeOverrideAttribute\(params BindMode\[\]\) {#Aspid_MVVM_BindModeOverrideAttribute__ctor_Aspid_MVVM_BindMode___}

Initializes a new instance of the [`BindModeOverrideAttribute`](Aspid.MVVM.BindModeOverrideAttribute.md) class with the specified binding modes.
If no modes are provided and neither [`BindModeOverrideAttribute.IsOne`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsOne) nor [`BindModeOverrideAttribute.IsTwo`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsTwo) is set, the behavior is equivalent to [`BindModeOverrideAttribute.IsAll`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsAll).
If [`BindModeOverrideAttribute.IsOne`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsOne) and [`BindModeOverrideAttribute.IsTwo`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsTwo) are both <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, the behavior is equivalent to [`BindModeOverrideAttribute.IsAll`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsAll).
If [`BindModeOverrideAttribute.IsOne`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsOne) or [`BindModeOverrideAttribute.IsTwo`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsTwo) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and modes are provided, the allowed modes are a combination of the specified modes and the modes defined by the properties.

```csharp
public BindModeOverrideAttribute(params BindMode[] modes)
```

#### Parameters

`modes` [BindMode](Aspid.MVVM.BindMode.md)\[\]

The binding modes that are allowed for the class.

## Properties

### IsAll {#Aspid_MVVM_BindModeOverrideAttribute_IsAll}

Indicates whether all binding modes (except [`BindMode.None`](Aspid.MVVM.BindMode.md)) are allowed.
If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, all modes except [`BindMode.None`](Aspid.MVVM.BindMode.md) are enabled.

```csharp
public bool IsAll { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsOne {#Aspid_MVVM_BindModeOverrideAttribute_IsOne}

Indicates whether only [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) are allowed.
If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only these two modes are enabled.

```csharp
public bool IsOne { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsTwo {#Aspid_MVVM_BindModeOverrideAttribute_IsTwo}

Indicates whether only [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) are allowed.
If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, only these two modes are enabled.

```csharp
public bool IsTwo { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Modes {#Aspid_MVVM_BindModeOverrideAttribute_Modes}

Gets the array of allowed binding modes for the class.

```csharp
public BindMode[] Modes { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)\[\]

