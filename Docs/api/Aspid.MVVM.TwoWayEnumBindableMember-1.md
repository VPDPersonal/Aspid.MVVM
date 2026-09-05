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

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)
for two-way enum bindings, supporting both strongly-typed and boxed-enum binders.

```csharp
public sealed class TwoWayEnumBindableMember<T> : TwoWayStructBindableMember<T, Enum>, IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct, Enum
```

#### Type Parameters

`T` 

The enum type of the bound value.

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

Initializes a new instance of the [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) class with the specified value and a setter action.

```csharp
public TwoWayEnumBindableMember(T value, Action<T> setValue)
```

#### Parameters

`value` T

The initial value of the bindable member.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

