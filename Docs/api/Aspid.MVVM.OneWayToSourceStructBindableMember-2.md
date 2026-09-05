---
title: "Class OneWayToSourceStructBindableMember<T, TBoxed>"
sidebar_label: "OneWayToSourceStructBindableMember<T, TBoxed>"
description: "Class OneWayToSourceStructBindableMember<T, TBoxed> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceStructBindableMember\<T, TBoxed\> {#Aspid_MVVM_OneWayToSourceStructBindableMember_2}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued one-way-to-source bindings that forwards
View-side value changes back to the ViewModel through a captured setter [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1);
additionally exposes the latest [`OneWayToSourceStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Value) and a [`OneWayToSourceStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Changed) event.
Accepts [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md), [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md), and
[`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md) in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) or [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) mode.

```csharp
public abstract class OneWayToSourceStructBindableMember<T, TBoxed> : IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The struct type of the bound value.

`TBoxed` 

The reference type used as the boxing target for <code class="typeparamref">T</code> (typically [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayToSourceStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md)

#### Implements

[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayToSourceStructBindableMember\(Action\<T\>\) {#Aspid_MVVM_OneWayToSourceStructBindableMember_2__ctor_System_Action__0__}

Initializes a new instance of the [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) class with the specified value setter action.

```csharp
protected OneWayToSourceStructBindableMember(Action<T> setValue)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Value}

Gets the current value.

```csharp
public T Value { get; }
```

#### Property Value

 T

### Changed {#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Changed}

Raised when the value changes.

```csharp
public event Action<T>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>?

