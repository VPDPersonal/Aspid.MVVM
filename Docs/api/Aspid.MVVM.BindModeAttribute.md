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

Limits the binding modes that the Inspector offers for a [`BindMode`](Aspid.MVVM.BindMode.md) field.

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



## Examples


```csharp
[BindMode(BindMode.OneWay, BindMode.OneTime)]
[SerializeField] private BindMode _mode;
```


```csharp
[BindMode(IsTwo = true)]
[SerializeField] private BindMode _mode;
```


## Remarks

<p>
The allowed modes are [`BindModeAttribute.Modes`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_Modes) plus the groups that [`BindModeAttribute.IsOne`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsOne) and [`BindModeAttribute.IsTwo`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsTwo) add; with none of
them, or with [`BindModeAttribute.IsAll`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsAll), every mode except [`BindMode.None`](Aspid.MVVM.BindMode.md) is allowed.
</p>
<p>The Inspector replaces a stored mode outside the allowed set with the first allowed one.</p>

## Constructors

### BindModeAttribute\(params BindMode\[\]\) {#Aspid_MVVM_BindModeAttribute__ctor_Aspid_MVVM_BindMode___}

Creates an attribute that allows <code class="paramref">modes</code>.

```csharp
public BindModeAttribute(params BindMode[] modes)
```

#### Parameters

`modes` [BindMode](Aspid.MVVM.BindMode.md)\[\]

The allowed modes; none to allow every mode unless [`BindModeAttribute.IsOne`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsOne) or [`BindModeAttribute.IsTwo`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_IsTwo) is set.

## Properties

### IsAll {#Aspid_MVVM_BindModeAttribute_IsAll}

Gets or sets a value indicating whether every mode except [`BindMode.None`](Aspid.MVVM.BindMode.md) is allowed.

```csharp
public bool IsAll { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsOne {#Aspid_MVVM_BindModeAttribute_IsOne}

Gets or sets a value indicating whether [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) are allowed in
addition to [`BindModeAttribute.Modes`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_Modes).

```csharp
public bool IsOne { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsTwo {#Aspid_MVVM_BindModeAttribute_IsTwo}

Gets or sets a value indicating whether [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) are
allowed in addition to [`BindModeAttribute.Modes`](Aspid.MVVM.BindModeAttribute.md#Aspid_MVVM_BindModeAttribute_Modes).

```csharp
public bool IsTwo { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Modes {#Aspid_MVVM_BindModeAttribute_Modes}

Gets the explicitly allowed modes.

```csharp
public BindMode[] Modes { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)\[\]

