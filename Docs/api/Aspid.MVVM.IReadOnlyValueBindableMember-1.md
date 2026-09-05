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

Represents a bindable member that exposes a read-only value and allows binders to be added.

```csharp
public interface IReadOnlyValueBindableMember<out T> : IBinderAdder
```

#### Type Parameters

`T` 

The type of the value being exposed.

#### Implements

[IBinderAdder](Aspid.MVVM.IBinderAdder.md)


## Properties

### Value {#Aspid_MVVM_IReadOnlyValueBindableMember_1_Value}

Gets the current value of the bindable member.

```csharp
T? Value { get; }
```

#### Property Value

 T?

