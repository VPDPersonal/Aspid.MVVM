---
title: "Class OneTimeEnumBindableMember<T>"
sidebar_label: "OneTimeEnumBindableMember<T>"
description: "Class OneTimeEnumBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeEnumBindableMember\<T\> {#Aspid_MVVM_OneTimeEnumBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

```csharp
public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>, IReadOnlyValueBindableMember<T>, IBinderAdder where T : struct, Enum
```

#### Type Parameters

`T`

The bound enum type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[OneTimeStructBindableMember\<T, Enum\>](Aspid.MVVM.OneTimeStructBindableMember-2.md) ←
[OneTimeEnumBindableMember\<T\>](Aspid.MVVM.OneTimeEnumBindableMember-1.md)

#### Implements

[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md),
[IBinderAdder](Aspid.MVVM.IBinderAdder.md)



## Constructors

### OneTimeEnumBindableMember\(T\) {#Aspid_MVVM_OneTimeEnumBindableMember_1__ctor__0_}

Creates a member holding <code class="paramref">value</code>.

```csharp
public OneTimeEnumBindableMember(T value)
```

#### Parameters

`value` T

The value to push to the binder.
