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

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued one-time bindings that pushes a single
[`OneTimeStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneTimeStructBindableMember-2.md#Aspid_MVVM_OneTimeStructBindableMember_2_Value) to the binder and then releases it; supports binders typed against
<code class="typeparamref">T</code>, <code class="typeparamref">TBoxed</code>, or [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md).

```csharp
public abstract class OneTimeStructBindableMember<T, TBoxed> : IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct, TBoxed where TBoxed : class
```

#### Type Parameters

`T` 

The struct type of the bound value.

`TBoxed` 

The reference type used as the boxing target for <code class="typeparamref">T</code> (typically [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype) or [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)).

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

Gets the binding mode for this member.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneTimeStructBindableMember_2_Value}

Gets the current value.

```csharp
public T Value { get; }
```

#### Property Value

 T

