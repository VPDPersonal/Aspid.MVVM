---
title: "Class OneTimeStructBindableMember<T>"
sidebar_label: "OneTimeStructBindableMember<T>"
description: "Class OneTimeStructBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeStructBindableMember\<T\> {#Aspid_MVVM_OneTimeStructBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype),
exposed as a per-type singleton via [`OneTimeStructBindableMember<T>.Get`](Aspid.MVVM.OneTimeStructBindableMember-1.md#Aspid_MVVM_OneTimeStructBindableMember_1_Get__0_).

```csharp
public sealed class OneTimeStructBindableMember<T> : OneTimeStructBindableMember<T, ValueType>, IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct
```

#### Type Parameters

`T` 

The struct type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeStructBindableMember\<T, ValueType\>](Aspid.MVVM.OneTimeStructBindableMember-2.md) ← 
[OneTimeStructBindableMember\<T\>](Aspid.MVVM.OneTimeStructBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Methods

### Get\(T\) {#Aspid_MVVM_OneTimeStructBindableMember_1_Get__0_}

Creates a reusable instance and assigns the provided value for one-time binding.

```csharp
public static OneTimeStructBindableMember<T> Get(T value)
```

#### Parameters

`value` T

The value to be provided to the binder.

#### Returns

 [OneTimeStructBindableMember](Aspid.MVVM.OneTimeStructBindableMember-1.md)\<T\>

A singleton instance of [`OneTimeStructBindableMember<T>`](Aspid.MVVM.OneTimeStructBindableMember-1.md) configured with the specified value.

