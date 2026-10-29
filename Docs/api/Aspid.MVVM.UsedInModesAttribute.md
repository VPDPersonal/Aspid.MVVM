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

Disables the field in the Inspector while the binder that holds it has a mode outside [`UsedInModesAttribute.Modes`](Aspid.MVVM.UsedInModesAttribute.md#Aspid_MVVM_UsedInModesAttribute_Modes).

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



## Examples


```csharp
[UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
[SerializeField] private string _format;
```


## Remarks

The field can sit on the binder or anywhere inside an object the binder serializes; the nearest binder above it decides.
With no binder above it or no modes, the field stays enabled.

## Constructors

### UsedInModesAttribute\(params BindMode\[\]\) {#Aspid_MVVM_UsedInModesAttribute__ctor_Aspid_MVVM_BindMode___}

Creates an attribute for a field used in <code class="paramref">modes</code>.

```csharp
public UsedInModesAttribute(params BindMode[] modes)
```

#### Parameters

`modes` [BindMode](Aspid.MVVM.BindMode.md)\[\]

The binding modes the field is used in.

## Properties

### Modes {#Aspid_MVVM_UsedInModesAttribute_Modes}

Gets the binding modes the field is used in.

```csharp
public IReadOnlyList<BindMode> Modes { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[BindMode](Aspid.MVVM.BindMode.md)\>

