---
title: "Interface IReadOnlyValueBindableMember<T>"
sidebar_label: "IReadOnlyValueBindableMember<T>"
description: "Interface IReadOnlyValueBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IReadOnlyValueBindableMember\<T\> {#Aspid_MVVM_IReadOnlyValueBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that exposes its current value.

```csharp
public interface IReadOnlyValueBindableMember<out T> : IBinderAdder
```

#### Type Parameters

`T` 

The bound value type.

#### Implements

[IBinderAdder](Aspid.MVVM.IBinderAdder.md)


## Properties

### Value {#Aspid_MVVM_IReadOnlyValueBindableMember_1_Value}

Gets the current value.

```csharp
T? Value { get; }
```

#### Property Value

 T?

