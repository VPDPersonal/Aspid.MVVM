---
title: "Class ReverseBinderInvalidCastException<T>"
sidebar_label: "ReverseBinderInvalidCastException<T>"
description: "Class ReverseBinderInvalidCastException<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ReverseBinderInvalidCastException\<T\> {#Aspid_MVVM_ReverseBinderInvalidCastException_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Exception thrown when a binder is not of the expected reverse binder type during a one-way-to-source binding operation.
Provides factory methods for generating descriptive error messages for class and struct reverse binders.

```csharp
public sealed class ReverseBinderInvalidCastException<T> : InvalidCastException, ISerializable
```

#### Type Parameters

`T` 

The expected reverse-bound value type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[SystemException](https://learn.microsoft.com/dotnet/api/system.systemexception) ← 
[InvalidCastException](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) ← 
[ReverseBinderInvalidCastException\<T\>](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)



## Methods

### Class\(IBinder\) {#Aspid_MVVM_ReverseBinderInvalidCastException_1_Class_Aspid_MVVM_IBinder_}

Throws a [`ReverseBinderInvalidCastException<T>`](Aspid.MVVM.ReverseBinderInvalidCastException-1.md) when a class binder does not implement
[`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md).

```csharp
public static ReverseBinderInvalidCastException<T> Class(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the type check.

#### Returns

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

This method never returns; the return type exists to support <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expressions.

#### Exceptions

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

Always thrown.

### Struct\<TBoxed\>\(IBinder\) {#Aspid_MVVM_ReverseBinderInvalidCastException_1_Struct__1_Aspid_MVVM_IBinder_}

Throws a [`ReverseBinderInvalidCastException<T>`](Aspid.MVVM.ReverseBinderInvalidCastException-1.md) when a struct binder does not implement
[`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) or [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md).

```csharp
public static ReverseBinderInvalidCastException<T> Struct<TBoxed>(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the type check.

#### Returns

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

This method never returns; the return type exists to support <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expressions.

#### Type Parameters

`TBoxed` 

The expected boxed type.

#### Exceptions

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

Always thrown.

