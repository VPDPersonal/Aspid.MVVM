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

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that pushes value changes from the ViewModel to subscribed
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) instances; additionally exposes a get/set
[`OneWayBindableMember<T>.Value`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Value) and a [`OneWayBindableMember<T>.Changed`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Changed) event. Accepts only [`BindMode.OneWay`](Aspid.MVVM.BindMode.md)
and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) binders.

```csharp
public sealed class OneWayBindableMember<T> : IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover
```

#### Type Parameters

`T` 

The reference type of the bound value.

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

Initializes a new instance of the [`OneWayBindableMember<T>`](Aspid.MVVM.OneWayBindableMember-1.md) class with the specified initial value.

```csharp
public OneWayBindableMember(T? value)
```

#### Parameters

`value` T?

The initial value of the bindable member.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is neither [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) nor [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

## Properties

### Mode {#Aspid_MVVM_OneWayBindableMember_1_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayBindableMember_1_Value}

Gets or sets the current value. Setting the value will trigger the [`OneWayBindableMember<T>.Changed`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Changed) event.

```csharp
public T? Value { get; set; }
```

#### Property Value

 T?

## Methods

### Invoke\(T?\) {#Aspid_MVVM_OneWayBindableMember_1_Invoke__0_}

Sets the current value and raises the [`OneWayBindableMember<T>.Changed`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Changed) event.

```csharp
public void Invoke(T? value)
```

#### Parameters

`value` T?

The new value to set and notify.

### Changed {#Aspid_MVVM_OneWayBindableMember_1_Changed}

Raised when the value changes.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

