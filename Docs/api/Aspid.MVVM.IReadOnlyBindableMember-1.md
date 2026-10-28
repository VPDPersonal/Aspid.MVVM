---
title: "Interface IReadOnlyBindableMember<T>"
sidebar_label: "IReadOnlyBindableMember<T>"
description: "Interface IReadOnlyBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IReadOnlyBindableMember\<T\> {#Aspid_MVVM_IReadOnlyBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that reports value changes.

```csharp
public interface IReadOnlyBindableMember<out T> : IReadOnlyValueBindableMember<T>, IBinderAdder
```

#### Type Parameters

`T` 

The bound value type.

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)


### Changed {#Aspid_MVVM_IReadOnlyBindableMember_1_Changed}

Occurs when the value changes.

```csharp
event Action<out T?>? Changed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

