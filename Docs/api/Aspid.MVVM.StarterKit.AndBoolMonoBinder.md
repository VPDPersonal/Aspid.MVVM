---
title: "Class AndBoolMonoBinder"
sidebar_label: "AndBoolMonoBinder"
description: "Class AndBoolMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AndBoolMonoBinder {#Aspid_MVVM_StarterKit_AndBoolMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) that forwards <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> only when every input
is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – And")]
public sealed class AndBoolMonoBinder : AggregatorMonoBinder<bool, bool>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[AggregatorMonoBinder\<bool, bool\>](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) ← 
[AndBoolMonoBinder](Aspid.MVVM.StarterKit.AndBoolMonoBinder.md)



## Methods

### Combine\(bool\[\]\) {#Aspid_MVVM_StarterKit_AndBoolMonoBinder_Combine_System_Boolean___}

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

