---
title: "Class FormatStringMonoBinder"
sidebar_label: "FormatStringMonoBinder"
description: "Class FormatStringMonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class FormatStringMonoBinder {#Aspid_MVVM_StarterKit_FormatStringMonoBinder}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`AggregatorMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) that formats the input strings into one line.

```csharp
[AddComponentMenu("Aspid/MVVM/Binders/Aggregator/Aggregator – Format String")]
public sealed class FormatStringMonoBinder : AggregatorMonoBinder<string, string>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[AggregatorMonoBinder\<string, string\>](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md) ← 
[FormatStringMonoBinder](Aspid.MVVM.StarterKit.FormatStringMonoBinder.md)



## Remarks

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> input formats as an empty string; a format that does not match the inputs is reported.

## Methods

### Combine\(string\[\]\) {#Aspid_MVVM_StarterKit_FormatStringMonoBinder_Combine_System_String___}

Combines the values every input has reported.

```csharp
protected override string Combine(string[] values)
```

#### Parameters

`values` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]

One value per input, in input order.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The value to forward.

