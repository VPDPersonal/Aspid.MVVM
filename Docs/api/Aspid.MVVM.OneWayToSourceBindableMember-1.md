---
title: "Class OneWayToSourceBindableMember<T>"
sidebar_label: "OneWayToSourceBindableMember<T>"
description: "Class OneWayToSourceBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceBindableMember\<T\> {#Aspid_MVVM_OneWayToSourceBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that forwards View-side value changes back to the ViewModel through a
captured setter [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1); additionally exposes the latest [`OneWayToSourceBindableMember<T>.Value`](Aspid.MVVM.OneWayToSourceBindableMember-1.md#Aspid_MVVM_OneWayToSourceBindableMember_1_Value) and a
[`OneWayToSourceBindableMember<T>.Changed`](Aspid.MVVM.OneWayToSourceBindableMember-1.md#Aspid_MVVM_OneWayToSourceBindableMember_1_Changed) event. Accepts only [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) and
[`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) reverse binders.

```csharp
public sealed class OneWayToSourceBindableMember<T> : IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover
```

#### Type Parameters

`T` 

The reference type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayToSourceBindableMember\<T\>](Aspid.MVVM.OneWayToSourceBindableMember-1.md)

#### Implements

[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayToSourceBindableMember\(Action\<T?\>\) {#Aspid_MVVM_OneWayToSourceBindableMember_1__ctor_System_Action__0__}

Initializes a new instance of the [`OneWayToSourceBindableMember<T>`](Aspid.MVVM.OneWayToSourceBindableMember-1.md) class with the specified value setter action.

```csharp
public OneWayToSourceBindableMember(Action<T?> setValue)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_OneWayToSourceBindableMember_1_Mode}

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayToSourceBindableMember_1_Value}

Gets the current value.

```csharp
public T? Value { get; }
```

#### Property Value

 T?

### Changed {#Aspid_MVVM_OneWayToSourceBindableMember_1_Changed}

Raised when the value changes.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

