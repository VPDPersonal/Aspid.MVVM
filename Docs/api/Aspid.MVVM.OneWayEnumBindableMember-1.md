---
title: "Class OneWayEnumBindableMember<T>"
sidebar_label: "OneWayEnumBindableMember<T>"
description: "Class OneWayEnumBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayEnumBindableMember\<T\> {#Aspid_MVVM_OneWayEnumBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

```csharp
public sealed class OneWayEnumBindableMember<T> : OneWayStructBindableMember<T, Enum>, IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, Enum
```

#### Type Parameters

`T` 

The bound enum type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayStructBindableMember\<T, Enum\>](Aspid.MVVM.OneWayStructBindableMember-2.md) ← 
[OneWayEnumBindableMember\<T\>](Aspid.MVVM.OneWayEnumBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayEnumBindableMember\(T\) {#Aspid_MVVM_OneWayEnumBindableMember_1__ctor__0_}

Creates a member holding <code class="paramref">value</code>.

```csharp
public OneWayEnumBindableMember(T value)
```

#### Parameters

`value` T

The initial value.

