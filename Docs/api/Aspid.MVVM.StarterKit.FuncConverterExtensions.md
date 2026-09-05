---
title: "Class FuncConverterExtensions"
sidebar_label: "FuncConverterExtensions"
description: "Class FuncConverterExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class FuncConverterExtensions {#Aspid_MVVM_StarterKit_FuncConverterExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Turns a function into a converter.

```csharp
public static class FuncConverterExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FuncConverterExtensions](Aspid.MVVM.StarterKit.FuncConverterExtensions.md)



## Methods

### ToConverter\<TFrom, TTo\>\(Func\<TFrom?, TTo?\>\) {#Aspid_MVVM_StarterKit_FuncConverterExtensions_ToConverter__2_System_Func___0___1__}

Wraps the specified function as an [`IConverter<T1, T2>`](Aspid.MVVM.StarterKit.IConverter-2.md).

```csharp
public static IConverter<TFrom?, TTo?> ToConverter<TFrom, TTo>(this Func<TFrom?, TTo?> converter)
```

#### Parameters

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom?, TTo?\>

The function to wrap.

#### Returns

 [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<TFrom?, TTo?\>

A converter that calls the function.

#### Type Parameters

`TFrom` 

The type the function accepts.

`TTo` 

The type it returns.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">converter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

