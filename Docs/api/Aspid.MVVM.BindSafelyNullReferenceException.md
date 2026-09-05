---
title: "Class BindSafelyNullReferenceException"
sidebar_label: "BindSafelyNullReferenceException"
description: "Class BindSafelyNullReferenceException — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindSafelyNullReferenceException {#Aspid_MVVM_BindSafelyNullReferenceException}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Exception thrown when a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder is encountered during a
[`BinderExtensions.BindSafely%60<T>`](Aspid.MVVM.BinderExtensions.md) operation.

```csharp
public sealed class BindSafelyNullReferenceException : NullReferenceException, ISerializable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Exception](https://learn.microsoft.com/dotnet/api/system.exception) ← 
[SystemException](https://learn.microsoft.com/dotnet/api/system.systemexception) ← 
[NullReferenceException](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception) ← 
[BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

#### Implements

[ISerializable](https://learn.microsoft.com/dotnet/api/system.runtime.serialization.iserializable)



## Constructors

### BindSafelyNullReferenceException\(\) {#Aspid_MVVM_BindSafelyNullReferenceException__ctor}

Initializes a new instance of the [`NullReferenceException`](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception) class, setting the [`Message`](https://learn.microsoft.com/dotnet/api/system.exception.message) property of the new instance to a system-supplied message that describes the error, such as "The value 'null' was found where an instance of an object was required." This message takes into account the current system culture.

```csharp
public BindSafelyNullReferenceException()
```

### BindSafelyNullReferenceException\(string\) {#Aspid_MVVM_BindSafelyNullReferenceException__ctor_System_String_}

Initializes a new instance of the [`NullReferenceException`](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception) class with a specified error message.

```csharp
public BindSafelyNullReferenceException(string message)
```

#### Parameters

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

A [`String`](https://learn.microsoft.com/dotnet/api/system.string) that describes the error. The content of <code class="paramref">message</code> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.

### BindSafelyNullReferenceException\(string, Exception\) {#Aspid_MVVM_BindSafelyNullReferenceException__ctor_System_String_System_Exception_}

Initializes a new instance of the [`NullReferenceException`](https://learn.microsoft.com/dotnet/api/system.nullreferenceexception) class with a specified error message and a reference to the inner exception that is the cause of this exception.

```csharp
public BindSafelyNullReferenceException(string message, Exception innerException)
```

#### Parameters

`message` [string](https://learn.microsoft.com/dotnet/api/system.string)

The error message that explains the reason for the exception.

`innerException` [Exception](https://learn.microsoft.com/dotnet/api/system.exception)

The exception that is the cause of the current exception. If the <code class="paramref">innerException</code> parameter is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, the current exception is raised in a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/try-catch">catch</a> block that handles the inner exception.

