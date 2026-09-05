---
title: "Class OneWayStructBindableMember<T>"
sidebar_label: "OneWayStructBindableMember<T>"
description: "Class OneWayStructBindableMember<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayStructBindableMember\<T\> {#Aspid_MVVM_OneWayStructBindableMember_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype)
for any value-type payload that does not need a more specific boxing target.

```csharp
public sealed class OneWayStructBindableMember<T> : OneWayStructBindableMember<T, ValueType>, IBindableMember<T>, IReadOnlyBindableMember<T>, IReadOnlyValueBindableMember<T>, IBinderAdder, IBinderRemover where T : struct
```

#### Type Parameters

`T` 

The struct type of the bound value.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OneWayStructBindableMember\<T, ValueType\>](Aspid.MVVM.OneWayStructBindableMember-2.md) ← 
[OneWayStructBindableMember\<T\>](Aspid.MVVM.OneWayStructBindableMember-1.md)

#### Implements

[IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md), 
[IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md), 
[IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md), 
[IBinderAdder](Aspid.MVVM.IBinderAdder.md), 
[IBinderRemover](Aspid.MVVM.IBinderRemover.md)



## Constructors

### OneWayStructBindableMember\(T\) {#Aspid_MVVM_OneWayStructBindableMember_1__ctor__0_}

Initializes a new instance of the [`OneWayStructBindableMember<T>`](Aspid.MVVM.OneWayStructBindableMember-1.md) class with the specified initial value.

```csharp
public OneWayStructBindableMember(T value)
```

#### Parameters

`value` T

The initial value of the bindable member.

