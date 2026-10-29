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

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

```csharp
public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>, IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct, Enum
```

#### Type Parameters

`T` 

The bound enum type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeStructBindableMember\<T, Enum\>](Aspid.MVVM.OneTimeStructBindableMember-2.md) ← 
[OneTimeEnumBindableMember\<T\>](Aspid.MVVM.OneTimeEnumBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Methods

### Get\(T\) {#Aspid_MVVM_OneTimeEnumBindableMember_1_Get__0_}

Returns the shared instance with [`OneTimeStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneTimeStructBindableMember-2.md#Aspid_MVVM_OneTimeStructBindableMember_2_Value) set to <code class="paramref">value</code>.

```csharp
public static OneTimeEnumBindableMember<T> Get(T value)
```

#### Parameters

`value` T

The value to push to the binder.

#### Returns

 [OneTimeEnumBindableMember](Aspid.MVVM.OneTimeEnumBindableMember-1.md)\<T\>

The shared instance for <code class="typeparamref">T</code>.

#### Remarks

Every call returns the same instance and overwrites its value, so bind it before the next call.

