---
title: "Class OneTimeStructBindableMember<T, TBoxed>"
sidebar_label: "OneTimeStructBindableMember<T, TBoxed>"
description: "Class OneTimeStructBindableMember<T, TBoxed> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeStructBindableMember\<T, TBoxed\> {#Aspid_MVVM_OneTimeStructBindableMember_2}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that pushes its value to a binder once and keeps no subscription,
boxing it for an [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) of <code class="typeparamref">TBoxed</code>.

```csharp
public abstract class OneTimeStructBindableMember<T, TBoxed> : IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The bound struct type.

`TBoxed` 

The boxed form of <code class="typeparamref">T</code>, such as [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneTimeStructBindableMember-2.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Fields

### GetMarker {#Aspid_MVVM_OneTimeStructBindableMember_2_GetMarker}

```csharp
protected static readonly ProfilerMarker GetMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Mode {#Aspid_MVVM_OneTimeStructBindableMember_2_Mode}

Gets the binding mode of the member; always [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneTimeStructBindableMember_2_Value}

Gets the value pushed to the next binder.

```csharp
public T Value { get; }
```

#### Property Value

 T

