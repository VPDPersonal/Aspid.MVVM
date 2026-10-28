---
title: "Class OneTimeBindableMember<T>"
sidebar_label: "OneTimeBindableMember<T>"
description: "Class OneTimeBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeBindableMember\<T\> {#Aspid_MVVM_OneTimeBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that pushes its value to a binder once and keeps no subscription.

```csharp
public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>, IBinderAdder
```

#### Type Parameters

`T` 

The bound value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneTimeBindableMember\<T\>](Aspid.MVVM.OneTimeBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Properties

### Mode {#Aspid_MVVM_OneTimeBindableMember_1_Mode}

Gets the binding mode of the member; always [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Value {#Aspid_MVVM_OneTimeBindableMember_1_Value}

Gets the value set by the last [`OneTimeBindableMember<T>.Get`](Aspid.MVVM.OneTimeBindableMember-1.md#Aspid_MVVM_OneTimeBindableMember_1_Get__0_) call.

```csharp
public T? Value { get; }
```

#### Property Value

 T?

## Methods

### Get\(T?\) {#Aspid_MVVM_OneTimeBindableMember_1_Get__0_}

Returns the shared instance with [`OneTimeBindableMember<T>.Value`](Aspid.MVVM.OneTimeBindableMember-1.md#Aspid_MVVM_OneTimeBindableMember_1_Value) set to <code class="paramref">value</code>.

```csharp
public static OneTimeBindableMember<T> Get(T? value)
```

#### Parameters

`value` T?

The value to push to the binder.

#### Returns

 [OneTimeBindableMember](Aspid.MVVM.OneTimeBindableMember-1.md)\<T\>

The shared instance for <code class="typeparamref">T</code>.

#### Remarks

Every call returns the same instance and overwrites its value, so bind it before the next call.

