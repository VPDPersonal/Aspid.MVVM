---
title: "Class TwoWayBindableMember<T>"
sidebar_label: "TwoWayBindableMember<T>"
description: "Class TwoWayBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayBindableMember\<T\> {#Aspid_MVVM_TwoWayBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that supports every [`BindMode`](Aspid.MVVM.BindMode.md) except [`BindMode.None`](Aspid.MVVM.BindMode.md),
dispatching forward updates through [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) and reverse updates
through [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) / [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md); additionally exposes a get/set
[`TwoWayBindableMember<T>.Value`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Value) and a [`TwoWayBindableMember<T>.Changed`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Changed) event.

```csharp
public sealed class TwoWayBindableMember<T> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover
```

#### Type Parameters

`T` 

The reference type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TwoWayBindableMember\<T\>](Aspid.MVVM.TwoWayBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### TwoWayBindableMember\(T?, Action\<T?\>\) {#Aspid_MVVM_TwoWayBindableMember_1__ctor__0_System_Action__0__}

Initializes a new instance of the [`TwoWayBindableMember<T>`](Aspid.MVVM.TwoWayBindableMember-1.md) class with the specified value and a setter action.

```csharp
public TwoWayBindableMember(T? value, Action<T?> setValue)
```

#### Parameters

`value` T?

The initial value of the bindable member.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_TwoWayBindableMember_1_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_TwoWayBindableMember_1_Value}

Gets or sets the current value. Setting the value will trigger the [`TwoWayBindableMember<T>.Changed`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Changed) event.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

## Methods

### Invoke\(T?\) {#Aspid_MVVM_TwoWayBindableMember_1_Invoke__0_}

Sets the current value and raises the [`TwoWayBindableMember<T>.Changed`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Changed) event.

```csharp
public void Invoke(T? value)
```

#### Parameters

`value` T?

The new value to set and notify.

### Changed {#Aspid_MVVM_TwoWayBindableMember_1_Changed}

Raised when the value changes.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

