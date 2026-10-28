---
title: "Interface IBindableMember<T>"
sidebar_label: "IBindableMember<T>"
description: "Interface IBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IBindableMember\<T\> {#Aspid_MVVM_IBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) whose value can also be set.

```csharp
public interface IBindableMember<T> : IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder
```

#### Type Parameters

`T` 

The bound value type.

#### Implements

[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)


## Properties

### Value {#Aspid_MVVM_IBindableMember_1_Value}

Sets the current value and raises [`IReadOnlyBindableMember<T>.Changed`](Aspid.MVVM.IReadOnlyBindableMember-1.md#Aspid_MVVM_IReadOnlyBindableMember_1_Changed).

```csharp
T? Value { set; }
```

#### Property Value

 T?

