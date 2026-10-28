---
title: "Class TwoWayEnumBindableMember<T>"
sidebar_label: "TwoWayEnumBindableMember<T>"
description: "Class TwoWayEnumBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayEnumBindableMember\<T\> {#Aspid_MVVM_TwoWayEnumBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

```csharp
public sealed class TwoWayEnumBindableMember<T> : TwoWayStructBindableMember<T, Enum>, IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, Enum
```

#### Type Parameters

`T` 

The bound enum type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TwoWayStructBindableMember\<T, Enum\>](Aspid.MVVM.TwoWayStructBindableMember-2.md) ← 
[TwoWayEnumBindableMember\<T\>](Aspid.MVVM.TwoWayEnumBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### TwoWayEnumBindableMember\(T, Action\<T\>\) {#Aspid_MVVM_TwoWayEnumBindableMember_1__ctor__0_System_Action__0__}

Creates a member holding <code class="paramref">value</code> that passes reported values to <code class="paramref">setValue</code>.

```csharp
public TwoWayEnumBindableMember(T value, Action<T> setValue)
```

#### Parameters

`value` T

The initial value.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The callback that writes a value reported by a reverse binder to the ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

