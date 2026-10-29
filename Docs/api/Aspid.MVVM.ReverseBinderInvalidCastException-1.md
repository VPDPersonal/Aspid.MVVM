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

[`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) thrown when a binder lacks the interface a bindable member receives values through.

```csharp
public sealed class ReverseBinderInvalidCastException<T> : InvalidCastException, ISerializable
```

#### Type Parameters

`T` 

The value type of the bindable member.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[SystemException](https://learn.microsoft.com/dotnet/api/system.systemexception) ← 
[InvalidCastException](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) ← 
[ReverseBinderInvalidCastException\<T\>](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)



## Remarks

The message names the binder type and, for a component, its GameObject path.

## Methods

### Class\(IBinder\) {#Aspid_MVVM_ReverseBinderInvalidCastException_1_Class_Aspid_MVVM_IBinder_}

Throws for a binder that implements neither [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) nor [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md).

```csharp
public static ReverseBinderInvalidCastException<T> Class(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the check.

#### Returns

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

Never returns; the return type allows a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expression at the call site.

#### Exceptions

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

Always.

### Struct\<TBoxed\>\(IBinder\) {#Aspid_MVVM_ReverseBinderInvalidCastException_1_Struct__1_Aspid_MVVM_IBinder_}

Throws for a binder that implements none of [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md), [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) of
<code class="typeparamref">TBoxed</code> and [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md).

```csharp
public static ReverseBinderInvalidCastException<T> Struct<TBoxed>(IBinder binder)
```

#### Parameters

`binder` [IBinder](Aspid.MVVM.IBinder.md)

The binder that failed the check.

#### Returns

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

Never returns; the return type allows a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/throw">throw</a> expression at the call site.

#### Type Parameters

`TBoxed` 

The boxed form of <code>T</code>.

#### Exceptions

 [ReverseBinderInvalidCastException](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)\<T\>

Always.

