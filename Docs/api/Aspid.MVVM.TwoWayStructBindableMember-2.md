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

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes value changes to its binders and passes values from its reverse binders
to a setter, also through binders of <code class="typeparamref">TBoxed</code>.

```csharp
public abstract class TwoWayStructBindableMember<T, TBoxed> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The bound struct type.

`TBoxed` 

The boxed form of <code class="typeparamref">T</code>, such as [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TwoWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.TwoWayStructBindableMember-2.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Remarks

A value reported by a reverse binder goes only to the setter; [`TwoWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Value) changes once the ViewModel sets it.

## Constructors

### TwoWayStructBindableMember\(T, Action\<T\>\) {#Aspid_MVVM_TwoWayStructBindableMember_2__ctor__0_System_Action__0__}

Creates a member holding <code class="paramref">value</code> that passes reported values to <code class="paramref">setValue</code>.

```csharp
protected TwoWayStructBindableMember(T value, Action<T> setValue)
```

#### Parameters

`value` T

The initial value.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The callback that writes a value reported by a reverse binder to the ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_TwoWayStructBindableMember_2_Mode}

Gets the binding mode of the member; always [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_TwoWayStructBindableMember_2_Value}

Gets or sets the current value; setting it raises [`TwoWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Changed) even for an equal value.

```csharp
public T Value { get; set; }
```

#### Property Value

 T

## Methods

### Invoke\(T\) {#Aspid_MVVM_TwoWayStructBindableMember_2_Invoke__0_}

Sets [`TwoWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Value) to <code class="paramref">value</code>.

```csharp
public void Invoke(T value)
```

#### Parameters

`value` T

The new value.

### Changed {#Aspid_MVVM_TwoWayStructBindableMember_2_Changed}

Occurs when [`TwoWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Value) is set.

```csharp
public event Action<T>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>?

