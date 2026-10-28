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

[`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) thrown when a binder lacks the interface a bindable member pushes values through.

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



## Remarks

The message names the binder type and, for a component, its GameObject path.

## Methods

### Class\<T\>\(IBinder\) {#Aspid_MVVM_BinderInvalidCastException_Class__1_Aspid_MVVM_IBinder_}

Throws for a binder that implements neither [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) nor [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md).

```csharp
public static BinderInvalidCastException Class<T>(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the check.

#### Returns

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Never returns; the return type allows a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expression at the call site.

#### Type Parameters

`T` 

The value type of the bindable member.

#### Exceptions

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Always.

### Struct\<T, TBoxed\>\(IBinder\) {#Aspid_MVVM_BinderInvalidCastException_Struct__2_Aspid_MVVM_IBinder_}

Throws for a binder that implements none of [`IBinder<T>`](Aspid.MVVM.IBinder-1.md), [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) of
<code class="typeparamref">TBoxed</code> and [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md).

```csharp
public static BinderInvalidCastException Struct<T, TBoxed>(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the check.

#### Returns

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Never returns; the return type allows a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expression at the call site.

#### Type Parameters

`T` 

The struct type of the bindable member.

`TBoxed` 

The boxed form of <code class="typeparamref">T</code>.

#### Exceptions

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Always.

