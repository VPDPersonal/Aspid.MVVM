---
title: "Class TwoWayBindableMember<T>"
sidebar_label: "TwoWayBindableMember<T>"
description: "Class TwoWayBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayBindableMember\<T\> {#Aspid_MVVM_TwoWayBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes value changes to its binders and passes values from its reverse binders
to a setter.

```csharp
public sealed class TwoWayBindableMember<T> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover
```

#### Type Parameters

`T`

The bound value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[TwoWayBindableMember\<T\>](Aspid.MVVM.TwoWayBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md),
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md),
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md),
[IBinderAdder](Aspid.MVVM.IBinderAdder.md),
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Remarks

A value reported by a reverse binder goes only to the setter; [`TwoWayBindableMember<T>.Value`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Value) changes once the ViewModel sets it.

## Constructors

### TwoWayBindableMember\(T?, Action\<T?\>\) {#Aspid_MVVM_TwoWayBindableMember_1__ctor__0_System_Action__0__}

Creates a member holding <code class="paramref">value</code> that passes reported values to <code class="paramref">setValue</code>.

```csharp
public TwoWayBindableMember(T? value, Action<T?> setValue)
```

#### Parameters

`value` T?

The initial value.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

Writes values reported by the View to the ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_TwoWayBindableMember_1_Mode}

Gets the fixed mode [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_TwoWayBindableMember_1_Value}

Gets or sets the value and raises [`TwoWayBindableMember<T>.Changed`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Changed) on every assignment.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

## Methods

### Invoke\(T?\) {#Aspid_MVVM_TwoWayBindableMember_1_Invoke__0_}

Sets [`TwoWayBindableMember<T>.Value`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Value) to <code class="paramref">value</code>.

```csharp
public void Invoke(T? value)
```

#### Parameters

`value` T?

The new value.

### Changed {#Aspid_MVVM_TwoWayBindableMember_1_Changed}

Occurs when the member publishes a value.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

#### Remarks

The reported value may equal the previous value.
