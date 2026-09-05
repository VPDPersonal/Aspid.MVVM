---
title: "Class OrBoolMonoBinder"
sidebar_label: "OrBoolMonoBinder"
description: "Class OrBoolMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OrBoolMonoBinder {#Aspid_MVVM_StarterKit_OrBoolMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) that forwards <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when any input is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – Or")]
public sealed class OrBoolMonoBinder : AggregatorMonoBinder<bool, bool>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[AggregatorMonoBinder\<bool, bool\>](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) ← 
[OrBoolMonoBinder](Aspid.MVVM.StarterKit.OrBoolMonoBinder.md)



## Methods

### Combine\(bool\[\]\) {#Aspid_MVVM_StarterKit_OrBoolMonoBinder_Combine_System_Boolean___}

Combines the values every input has reported.

```csharp
protected override bool Combine(bool[] values)
```

#### Parameters

`values` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\[\]

One value per input, in input order.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The value to forward.

