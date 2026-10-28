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

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes every value change to its binders, boxing it for an
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) of <code class="typeparamref">TBoxed</code>.

```csharp
public abstract class OneWayStructBindableMember<T, TBoxed> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The bound struct type.

`TBoxed` 

The boxed form of <code class="typeparamref">T</code>, such as [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

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

Creates a member holding <code class="paramref">value</code>.

```csharp
protected OneWayStructBindableMember(T value)
```

#### Parameters

`value` T

The initial value.

## Properties

### Mode {#Aspid_MVVM_OneWayStructBindableMember_2_Mode}

Gets the binding mode of the member; always [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayStructBindableMember_2_Value}

Gets or sets the current value; setting it raises [`OneWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Changed) even for an equal value.

```csharp
public T Value { get; set; }
```

#### Property Value

 T

## Methods

### Invoke\(T\) {#Aspid_MVVM_OneWayStructBindableMember_2_Invoke__0_}

Sets [`OneWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Value) to <code class="paramref">value</code>.

```csharp
public void Invoke(T value)
```

#### Parameters

`value` T

The new value.

### Changed {#Aspid_MVVM_OneWayStructBindableMember_2_Changed}

Occurs when [`OneWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Value) is set.

```csharp
public event Action<T>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>?

