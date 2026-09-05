---
title: "Struct NumberReverseChannel"
sidebar_label: "NumberReverseChannel"
description: "Struct NumberReverseChannel — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct NumberReverseChannel {#Aspid_MVVM_StarterKit_NumberReverseChannel}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Holds the subscriptions behind the four numeric events of an [`INumberReverseBinder`](Aspid.MVVM.StarterKit.INumberReverseBinder.md)
and raises them together.

```csharp
public struct NumberReverseChannel
```



## Remarks

Keep it as a mutable field and expose it through [`INumberReverseBinder.Channel`](Aspid.MVVM.StarterKit.INumberReverseBinder.md#Aspid_MVVM_StarterKit_INumberReverseBinder_Channel).

## Properties

### HasFloatingPointListeners {#Aspid_MVVM_StarterKit_NumberReverseChannel_HasFloatingPointListeners}

Indicates whether [`NumberReverseChannel.FloatValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_FloatValueChanged) or [`NumberReverseChannel.DoubleValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_DoubleValueChanged) has a subscriber.

```csharp
public bool HasFloatingPointListeners { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### HasIntegerListeners {#Aspid_MVVM_StarterKit_NumberReverseChannel_HasIntegerListeners}

Indicates whether [`NumberReverseChannel.IntValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_IntValueChanged) or [`NumberReverseChannel.LongValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_LongValueChanged) has a subscriber.

```csharp
public bool HasIntegerListeners { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### Raise\(int\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_Raise_System_Int32_}

Raises all four events with <code class="paramref">value</code>.

```csharp
public void Raise(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The value read from the View.

### Raise\(long\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_Raise_System_Int64_}

Raises all four events with <code class="paramref">value</code>, saturating at the [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) bounds.

```csharp
public void Raise(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value read from the View.

### Raise\(float\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_Raise_System_Single_}

Raises all four events with <code class="paramref">value</code>, saturating at each type's bounds.
Integer events receive the value truncated toward zero, or zero for a NaN.

```csharp
public void Raise(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value read from the View.

### Raise\(double\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_Raise_System_Double_}

Raises all four events with <code class="paramref">value</code>, saturating at each type's bounds.
Integer events receive the value truncated toward zero, or zero for a NaN.

```csharp
public void Raise(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value read from the View.

### RaiseFloatingPoint\(double\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_RaiseFloatingPoint_System_Double_}

Raises only [`NumberReverseChannel.FloatValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_FloatValueChanged) and [`NumberReverseChannel.DoubleValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_DoubleValueChanged), saturating at the [`Single`](https://learn.microsoft.com/dotnet/api/system.single) bounds.

```csharp
public void RaiseFloatingPoint(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value read from the View.

### RaiseIntegers\(long\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_RaiseIntegers_System_Int64_}

Raises only [`NumberReverseChannel.IntValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_IntValueChanged) and [`NumberReverseChannel.LongValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_LongValueChanged), saturating at the [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) bounds.

```csharp
public void RaiseIntegers(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The value read from the View.

### RaiseIntegers\(double\) {#Aspid_MVVM_StarterKit_NumberReverseChannel_RaiseIntegers_System_Double_}

Raises only [`NumberReverseChannel.IntValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_IntValueChanged) and [`NumberReverseChannel.LongValueChanged`](Aspid.MVVM.StarterKit.NumberReverseChannel.md#Aspid_MVVM_StarterKit_NumberReverseChannel_LongValueChanged), saturating at each type's bounds.
The value is truncated toward zero; a NaN arrives as zero.

```csharp
public void RaiseIntegers(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The value read from the View.

### DoubleValueChanged {#Aspid_MVVM_StarterKit_NumberReverseChannel_DoubleValueChanged}

Raised with the View value for [`Double`](https://learn.microsoft.com/dotnet/api/system.double) subscribers.

```csharp
public event Action<double>? DoubleValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>?

### FloatValueChanged {#Aspid_MVVM_StarterKit_NumberReverseChannel_FloatValueChanged}

Raised with the View value for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) subscribers.

```csharp
public event Action<float>? FloatValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[float](https://learn.microsoft.com/dotnet/api/system.single)\>?

### IntValueChanged {#Aspid_MVVM_StarterKit_NumberReverseChannel_IntValueChanged}

Raised with the View value for [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) subscribers.

```csharp
public event Action<int>? IntValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

### LongValueChanged {#Aspid_MVVM_StarterKit_NumberReverseChannel_LongValueChanged}

Raised with the View value for [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) subscribers.

```csharp
public event Action<long>? LongValueChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[long](https://learn.microsoft.com/dotnet/api/system.int64)\>?

