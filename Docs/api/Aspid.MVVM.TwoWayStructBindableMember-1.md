---
title: "Class TwoWayStructBindableMember<T>"
sidebar_label: "TwoWayStructBindableMember<T>"
description: "Class TwoWayStructBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayStructBindableMember\<T\> {#Aspid_MVVM_TwoWayStructBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype)
for any value-type payload that does not need a more specific boxing target.

```csharp
public sealed class TwoWayStructBindableMember<T> : TwoWayStructBindableMember<T, ValueType>, IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct
```

#### Type Parameters

`T` 

The struct type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TwoWayStructBindableMember\<T, ValueType\>](Aspid.MVVM.TwoWayStructBindableMember-2.md) ← 
[TwoWayStructBindableMember\<T\>](Aspid.MVVM.TwoWayStructBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### TwoWayStructBindableMember\(T, Action\<T\>\) {#Aspid_MVVM_TwoWayStructBindableMember_1__ctor__0_System_Action__0__}

Initializes a new instance of the [`TwoWayStructBindableMember<T>`](Aspid.MVVM.TwoWayStructBindableMember-1.md) class with the specified value and a setter action.

```csharp
public TwoWayStructBindableMember(T value, Action<T> setValue)
```

#### Parameters

`value` T

The initial value of the bindable member.

`setValue` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

The action used to set the value when the event is triggered.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">setValue</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

