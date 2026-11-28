---
title: "Class OneWayToSourceBindableMember<T>"
sidebar_label: "OneWayToSourceBindableMember<T>"
description: "Class OneWayToSourceBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceBindableMember\<T\> {#Aspid_MVVM_OneWayToSourceBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) that passes values from its reverse binders to a setter.

```csharp
public sealed class OneWayToSourceBindableMember<T> : IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover
```

#### Type Parameters

`T`

The bound value type.

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

Creates a member that passes reported values to <code class="paramref">setValue</code>.

```csharp
public OneWayToSourceBindableMember(Action<T?> setValue)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

Writes values reported by the View to the ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Mode {#Aspid_MVVM_OneWayToSourceBindableMember_1_Mode}

Gets the fixed mode [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneWayToSourceBindableMember_1_Value}

Gets the last value reported by a reverse binder.

```csharp
public T? Value { get; }
```

#### Property Value

 T?

### Changed {#Aspid_MVVM_OneWayToSourceBindableMember_1_Changed}

Occurs when the member publishes a value.

```csharp
public event Action<T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

#### Remarks

The reported value may equal the previous value.
