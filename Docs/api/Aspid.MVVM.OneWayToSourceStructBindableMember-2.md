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

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) that passes values from its reverse binders to a setter, unboxing them from an
[`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) of <code class="typeparamref">TBoxed</code>.

```csharp
public abstract class OneWayToSourceStructBindableMember<T, TBoxed> : IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The bound struct type.

`TBoxed` 

The boxed form of <code class="typeparamref">T</code>, such as [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

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

Creates a member that passes reported values to <code class="paramref">setValue</code>.

```csharp
protected OneWayToSourceStructBindableMember(Action<T> setValue)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The callback that writes a value reported by a reverse binder to the ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Mode}

Gets the binding mode of the member; always [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Value}

Gets the last value reported by a reverse binder.

```csharp
public T Value { get; }
```

#### Property Value

 T

### Changed {#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Changed}

Occurs after a reverse binder reports a value and the setter has run.

```csharp
public event Action<T>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>?

