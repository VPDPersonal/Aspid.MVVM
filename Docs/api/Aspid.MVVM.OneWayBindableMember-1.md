---
title: "Class OneWayBindableMember<T>"
sidebar_label: "OneWayBindableMember<T>"
description: "Class OneWayBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayBindableMember\<T\> {#Aspid_MVVM_OneWayBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes every value change to its binders.

```csharp
public sealed class OneWayBindableMember<T> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover
```

#### Type Parameters

`T` 

The bound value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayBindableMember\<T\>](Aspid.MVVM.OneWayBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayBindableMember\(T?\) {#Aspid_MVVM_OneWayBindableMember_1__ctor__0_}

Creates a member holding <code class="paramref">value</code>.

```csharp
public OneWayBindableMember(T? value)
```

#### Parameters

`value` T?

The initial value.

## Properties

### Mode {#Aspid_MVVM_OneWayBindableMember_1_Mode}

Gets the binding mode of the member; always [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayBindableMember_1_Value}

Gets or sets the current value; setting it raises [`OneWayBindableMember<T>.Changed`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Changed) even for an equal value.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

## Methods

### Invoke\(T?\) {#Aspid_MVVM_OneWayBindableMember_1_Invoke__0_}

Sets [`OneWayBindableMember<T>.Value`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Value) to <code class="paramref">value</code>.

```csharp
public void Invoke(T? value)
```

#### Parameters

`value` T?

The new value.

### Changed {#Aspid_MVVM_OneWayBindableMember_1_Changed}

Occurs when [`OneWayBindableMember<T>.Value`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Value) is set.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

