---
title: "Class OneWayToSourceStructBindableMember<T>"
sidebar_label: "OneWayToSourceStructBindableMember<T>"
description: "Class OneWayToSourceStructBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceStructBindableMember\<T\> {#Aspid_MVVM_OneWayToSourceStructBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

```csharp
public sealed class OneWayToSourceStructBindableMember<T> : OneWayToSourceStructBindableMember<T, ValueType>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct
```

#### Type Parameters

`T`

The bound struct type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[OneWayToSourceStructBindableMember\<T, ValueType\>](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) ←
[OneWayToSourceStructBindableMember\<T\>](Aspid.MVVM.OneWayToSourceStructBindableMember-1.md)

#### Implements

[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md),
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md),
[IBinderAdder](Aspid.MVVM.IBinderAdder.md),
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayToSourceStructBindableMember\(Action\<T\>\) {#Aspid_MVVM_OneWayToSourceStructBindableMember_1__ctor_System_Action__0__}

Creates a member that passes reported values to <code class="paramref">setValue</code>.

```csharp
public OneWayToSourceStructBindableMember(Action<T> setValue)
```

#### Parameters

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

Writes values reported by the View to the ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
