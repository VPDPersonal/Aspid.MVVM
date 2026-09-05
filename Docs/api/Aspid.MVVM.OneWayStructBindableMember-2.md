---
title: "Class OneWayStructBindableMember<T, TBoxed>"
sidebar_label: "OneWayStructBindableMember<T, TBoxed>"
description: "Class OneWayStructBindableMember<T, TBoxed> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayStructBindableMember\<T, TBoxed\> {#Aspid_MVVM_OneWayStructBindableMember_2}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued one-way bindings that dispatches changes to
both [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) subscribers — the latter receive the
value pre-boxed as <code class="typeparamref">TBoxed</code>. Additionally exposes a get/set [`OneWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Value)
and a [`OneWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Changed) event. Accepts only [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and
[`BindMode.OneTime`](Aspid.MVVM.BindMode.md) binders.

```csharp
public abstract class OneWayStructBindableMember<T, TBoxed> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The struct type of the bound value.

`TBoxed` 

The reference type used as the boxing target for <code class="typeparamref">T</code> (typically [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayStructBindableMember-2.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayStructBindableMember\(T\) {#Aspid_MVVM_OneWayStructBindableMember_2__ctor__0_}

Initializes a new instance of the [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) class with the specified initial value.

```csharp
protected OneWayStructBindableMember(T value)
```

#### Parameters

`value` T

The initial value of the bindable member.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

## Properties

### Mode {#Aspid_MVVM_OneWayStructBindableMember_2_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayStructBindableMember_2_Value}

Gets or sets the current value. Setting the value will trigger the [`OneWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Changed) event.

```csharp
public T Value { get; set; }
```

#### Property Value

 T

## Methods

### Invoke\(T\) {#Aspid_MVVM_OneWayStructBindableMember_2_Invoke__0_}

Sets the current value and raises the [`OneWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Changed) event.

```csharp
public void Invoke(T value)
```

#### Parameters

`value` T

The new value to set and notify.

### Changed {#Aspid_MVVM_OneWayStructBindableMember_2_Changed}

Raised when the value changes.

```csharp
public event Action<T>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>?

