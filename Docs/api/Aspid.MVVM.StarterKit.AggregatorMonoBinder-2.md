---
title: "Class AggregatorMonoBinder<TInput, TResult>"
sidebar_label: "AggregatorMonoBinder<TInput, TResult>"
description: "Class AggregatorMonoBinder<TInput, TResult> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AggregatorMonoBinder\<TInput, TResult\> {#Aspid_MVVM_StarterKit_AggregatorMonoBinder_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) that combines the values of several
[`AggregatorInputMonoBinder<T1, T2>`](Aspid.MVVM.StarterKit.AggregatorInputMonoBinder-2.md) components into one [`UnityEvent<T>`](https://docs.unity3d.com/ScriptReference/Events-UnityEvent.html).

```csharp
public abstract class AggregatorMonoBinder<TInput, TResult> : MonoBehaviour
```

#### Type Parameters

`TInput` 

The type each input contributes.

`TResult` 

The type of the combined value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[AggregatorMonoBinder\<TInput, TResult\>](Aspid.MVVM.StarterKit.AggregatorMonoBinder-2.md)



## Remarks

Nothing is forwarded until every input has reported at least once.

## Properties

### InputCount {#Aspid_MVVM_StarterKit_AggregatorMonoBinder_2_InputCount}

Gets the number of inputs the aggregator expects.

```csharp
public int InputCount { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### ClearInput\(int\) {#Aspid_MVVM_StarterKit_AggregatorMonoBinder_2_ClearInput_System_Int32_}

Forgets one input's value, so it has to report again before the next combine.

```csharp
public void ClearInput(int index)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The input index.

### Combine\(TInput\[\]\) {#Aspid_MVVM_StarterKit_AggregatorMonoBinder_2_Combine__0___}

Combines the values every input has reported.

```csharp
protected abstract TResult Combine(TInput[] values)
```

#### Parameters

`values` TInput\[\]

One value per input, in input order.

#### Returns

 TResult

The value to forward.

### SetInput\(int, TInput\) {#Aspid_MVVM_StarterKit_AggregatorMonoBinder_2_SetInput_System_Int32__0_}

Stores one input's value and forwards the combined result once every input has reported.

```csharp
public void SetInput(int index, TInput value)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The input index.

`value` TInput

The value that input received.

