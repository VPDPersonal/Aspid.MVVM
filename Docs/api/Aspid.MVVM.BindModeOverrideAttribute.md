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

Replaces the [`BindModeAttribute`](Aspid.MVVM.BindModeAttribute.md) limits of every [`BindMode`](Aspid.MVVM.BindMode.md) field of the class and its subclasses in the Inspector.

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



## Examples


```csharp
[BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
public class FillAmountBinder : MonoBinder { }
```


## Remarks

<p>
The allowed modes are [`BindModeOverrideAttribute.Modes`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_Modes) plus the groups that [`BindModeOverrideAttribute.IsOne`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsOne) and [`BindModeOverrideAttribute.IsTwo`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsTwo) add; with none of
them, or with [`BindModeOverrideAttribute.IsAll`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsAll), every mode except [`BindMode.None`](Aspid.MVVM.BindMode.md) is allowed.
</p>
<p>The Inspector replaces a stored mode outside the allowed set with the first allowed one.</p>
<p>A subclass with its own [`BindModeOverrideAttribute`](Aspid.MVVM.BindModeOverrideAttribute.md) uses that one instead.</p>

## Constructors

### BindModeOverrideAttribute\(params BindMode\[\]\) {#Aspid_MVVM_BindModeOverrideAttribute__ctor_Aspid_MVVM_BindMode___}

Creates an attribute that allows <code class="paramref">modes</code>.

```csharp
public BindModeOverrideAttribute(params BindMode[] modes)
```

#### Parameters

`modes` [BindMode](Aspid.MVVM.BindMode.md)\[\]

The allowed modes; none to allow every mode unless [`BindModeOverrideAttribute.IsOne`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsOne) or [`BindModeOverrideAttribute.IsTwo`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_IsTwo) is set.

## Properties

### IsAll {#Aspid_MVVM_BindModeOverrideAttribute_IsAll}

Gets or sets a value indicating whether every mode except [`BindMode.None`](Aspid.MVVM.BindMode.md) is allowed.

```csharp
public bool IsAll { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsOne {#Aspid_MVVM_BindModeOverrideAttribute_IsOne}

Gets or sets a value indicating whether [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) are allowed in
addition to [`BindModeOverrideAttribute.Modes`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_Modes).

```csharp
public bool IsOne { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsTwo {#Aspid_MVVM_BindModeOverrideAttribute_IsTwo}

Gets or sets a value indicating whether [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) are
allowed in addition to [`BindModeOverrideAttribute.Modes`](Aspid.MVVM.BindModeOverrideAttribute.md#Aspid_MVVM_BindModeOverrideAttribute_Modes).

```csharp
public bool IsTwo { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Modes {#Aspid_MVVM_BindModeOverrideAttribute_Modes}

Gets the explicitly allowed modes.

```csharp
public BindMode[] Modes { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)\[\]

