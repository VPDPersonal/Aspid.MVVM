---
title: "Class OneTimeEnumBindableMember<T>"
sidebar_label: "OneTimeEnumBindableMember<T>"
description: "Class OneTimeEnumBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeEnumBindableMember\<T\> {#Aspid_MVVM_OneTimeEnumBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum),
exposed as a per-type singleton via [`OneTimeEnumBindableMember<T>.Get`](Aspid.MVVM.OneTimeEnumBindableMember-1.md#Aspid_MVVM_OneTimeEnumBindableMember_1_Get__0_) for one-time enum bindings.

```csharp
public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>, IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct, Enum
```

#### Type Parameters

`T` 

The enum type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeStructBindableMember\<T, Enum\>](Aspid.MVVM.OneTimeStructBindableMember-2.md) ← 
[OneTimeEnumBindableMember\<T\>](Aspid.MVVM.OneTimeEnumBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Methods

### Get\(T\) {#Aspid_MVVM_OneTimeEnumBindableMember_1_Get__0_}

Creates a reusable instance and assigns the provided enum value for one-time binding.

```csharp
public static OneTimeEnumBindableMember<T> Get(T value)
```

#### Parameters

`value` T

The enum value to provide to the binder.

#### Returns

 [OneTimeEnumBindableMember](Aspid.MVVM.OneTimeEnumBindableMember-1.md)\<T\>

A singleton instance of [`OneTimeEnumBindableMember<T>`](Aspid.MVVM.OneTimeEnumBindableMember-1.md) configured with the specified value.

