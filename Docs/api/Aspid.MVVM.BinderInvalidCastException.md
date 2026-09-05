---
title: "Class BinderInvalidCastException"
sidebar_label: "BinderInvalidCastException"
description: "Class BinderInvalidCastException — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BinderInvalidCastException {#Aspid_MVVM_BinderInvalidCastException}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Exception thrown when a binder is not of the expected type during a binding operation.
Provides factory methods for generating descriptive error messages for class and struct binders.

```csharp
public sealed class BinderInvalidCastException : InvalidCastException, ISerializable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[SystemException](https://learn.microsoft.com/dotnet/api/system.systemexception) ← 
[InvalidCastException](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) ← 
[BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)



## Methods

### Class\<T\>\(IBinder\) {#Aspid_MVVM_BinderInvalidCastException_Class__1_Aspid_MVVM_IBinder_}

Throws a [`BinderInvalidCastException`](Aspid.MVVM.BinderInvalidCastException.md) when a class binder does not implement
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) or [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md).

```csharp
public static BinderInvalidCastException Class<T>(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the type check.

#### Returns

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

This method never returns; the return type exists to support <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expressions.

#### Type Parameters

`T` 

The expected bound value type.

#### Exceptions

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Always thrown.

### Struct\<T, TBoxed\>\(IBinder\) {#Aspid_MVVM_BinderInvalidCastException_Struct__2_Aspid_MVVM_IBinder_}

Throws a [`BinderInvalidCastException`](Aspid.MVVM.BinderInvalidCastException.md) when a struct binder does not implement
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md), [`IBinder<T>`](Aspid.MVVM.IBinder-1.md), or [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md).

```csharp
public static BinderInvalidCastException Struct<T, TBoxed>(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the type check.

#### Returns

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

This method never returns; the return type exists to support <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expressions.

#### Type Parameters

`T` 

The expected unboxed struct type.

`TBoxed` 

The expected boxed type.

#### Exceptions

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Always thrown.

