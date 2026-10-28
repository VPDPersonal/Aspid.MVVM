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

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

```csharp
public sealed class OneTimeStructBindableMember<T> : OneTimeStructBindableMember<T, ValueType>, IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct
```

#### Type Parameters

`T` 

The bound struct type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeStructBindableMember\<T, ValueType\>](Aspid.MVVM.OneTimeStructBindableMember-2.md) ← 
[OneTimeStructBindableMember\<T\>](Aspid.MVVM.OneTimeStructBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Methods

### Get\(T\) {#Aspid_MVVM_OneTimeStructBindableMember_1_Get__0_}

Returns the shared instance with [`OneTimeStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneTimeStructBindableMember-2.md#Aspid_MVVM_OneTimeStructBindableMember_2_Value) set to <code class="paramref">value</code>.

```csharp
public static OneTimeStructBindableMember<T> Get(T value)
```

#### Parameters

`value` T

The value to push to the binder.

#### Returns

 [OneTimeStructBindableMember](Aspid.MVVM.OneTimeStructBindableMember-1.md)\<T\>

The shared instance for <code class="typeparamref">T</code>.

#### Remarks

Every call returns the same instance and overwrites its value, so bind it before the next call.

