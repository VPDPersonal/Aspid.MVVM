---
title: "Class OneTimeBindableMember<T>"
sidebar_label: "OneTimeBindableMember<T>"
description: "Class OneTimeBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeBindableMember\<T\> {#Aspid_MVVM_OneTimeBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that pushes its value to a binder once and keeps no subscription.

```csharp
public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>, IBinderAdder
```

#### Type Parameters

`T`

The bound value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[OneTimeBindableMember\<T\>](Aspid.MVVM.OneTimeBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md),
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Remarks

A [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) binder receives the initial value without reporting changes back to the ViewModel.

## Constructors

### OneTimeBindableMember\(T?\) {#Aspid_MVVM_OneTimeBindableMember_1__ctor__0_}

Creates a member holding <code class="paramref">value</code>.

```csharp
public OneTimeBindableMember(T? value)
```

#### Parameters

`value` T?

The value to push to the binder.

## Properties

### Mode {#Aspid_MVVM_OneTimeBindableMember_1_Mode}

Gets the fixed mode [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneTimeBindableMember_1_Value}

Gets or sets the value sent to each binder when it attaches.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?
