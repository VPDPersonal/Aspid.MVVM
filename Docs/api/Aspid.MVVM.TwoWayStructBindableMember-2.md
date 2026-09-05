---
title: "Class TwoWayStructBindableMember<T, TBoxed>"
sidebar_label: "TwoWayStructBindableMember<T, TBoxed>"
description: "Class TwoWayStructBindableMember<T, TBoxed> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayStructBindableMember\<T, TBoxed\> {#Aspid_MVVM_TwoWayStructBindableMember_2}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued two-way bindings that supports every
[`BindMode`](Aspid.MVVM.BindMode.md) except [`BindMode.None`](Aspid.MVVM.BindMode.md), dispatching forward updates through
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) and reverse updates
through [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) / [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) / [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md).
Additionally exposes a get/set [`TwoWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Value) and a [`TwoWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Changed) event.

```csharp
public abstract class TwoWayStructBindableMember<T, TBoxed> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The struct type of the bound value.

`TBoxed` 

The reference type used as the boxing target for <code class="typeparamref">T</code> (typically [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TwoWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.TwoWayStructBindableMember-2.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### TwoWayStructBindableMember\(T, Action\<T\>\) {#Aspid_MVVM_TwoWayStructBindableMember_2__ctor__0_System_Action__0__}

Initializes a new instance of the [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) class with the specified value and a setter action.

```csharp
protected TwoWayStructBindableMember(T value, Action<T> setValue)
```

#### Parameters

`value` T

The initial value of the bindable member.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_TwoWayStructBindableMember_2_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_TwoWayStructBindableMember_2_Value}

Gets or sets the current value. Setting the value will trigger the [`TwoWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Changed) event.

```csharp
public T Value { get; set; }
```

#### Property Value

 T

## Methods

### Invoke\(T\) {#Aspid_MVVM_TwoWayStructBindableMember_2_Invoke__0_}

Sets the current value and raises the [`TwoWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Changed) event.

```csharp
public void Invoke(T value)
```

#### Parameters

`value` T

The new value to set and notify.

### Changed {#Aspid_MVVM_TwoWayStructBindableMember_2_Changed}

Raised when the value changes.

```csharp
public event Action<T>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>?

