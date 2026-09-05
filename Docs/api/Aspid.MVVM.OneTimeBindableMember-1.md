---
title: "Class OneTimeBindableMember<T>"
sidebar_label: "OneTimeBindableMember<T>"
description: "Class OneTimeBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeBindableMember\<T\> {#Aspid_MVVM_OneTimeBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) exposed as a per-type singleton that pushes a single [`OneTimeBindableMember<T>.Value`](Aspid.MVVM.OneTimeBindableMember-1.md#Aspid_MVVM_OneTimeBindableMember_1_Value)
to the binder once and then releases it; rejects every [`BindMode`](Aspid.MVVM.BindMode.md) other than
[`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>, IBinderAdder
```

#### Type Parameters

`T` 

The reference type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeBindableMember\<T\>](Aspid.MVVM.OneTimeBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Properties

### Mode {#Aspid_MVVM_OneTimeBindableMember_1_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneTimeBindableMember_1_Value}

Gets the current value.

```csharp
public T? Value { get; }
```

#### Property Value

 T?

## Methods

### Get\(T?\) {#Aspid_MVVM_OneTimeBindableMember_1_Get__0_}

Creates a reusable instance and assigns the provided value for one-time binding.

```csharp
public static OneTimeBindableMember<T> Get(T? value)
```

#### Parameters

`value` T?

The value to be provided to the binder.

#### Returns

 [OneTimeBindableMember](Aspid.MVVM.OneTimeBindableMember-1.md)\<T\>

A singleton instance of [`OneTimeBindableMember<T>`](Aspid.MVVM.OneTimeBindableMember-1.md) configured with the specified value.

