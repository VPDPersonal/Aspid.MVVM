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

Represents a bindable member that allows setting a value and notifies listeners when the value changes.

```csharp
public interface IBindableMember<T> : IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder
```

#### Type Parameters

`T` 

The type of the value being bound.

#### Implements

[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)


## Properties

### Value {#Aspid_MVVM_IBindableMember_1_Value}

Sets the value of the bindable member.

```csharp
T? Value { set; }
```

#### Property Value

 T?

