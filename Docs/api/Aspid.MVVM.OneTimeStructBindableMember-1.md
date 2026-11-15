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



## Constructors

### OneTimeStructBindableMember\(T\) {#Aspid_MVVM_OneTimeStructBindableMember_1__ctor__0_}

Creates a member holding <code class="paramref">value</code>.

```csharp
public OneTimeStructBindableMember(T value)
```

#### Parameters

`value` T

The value to push to the binder.

