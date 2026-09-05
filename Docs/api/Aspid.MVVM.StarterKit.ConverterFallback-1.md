---
title: "Struct ConverterFallback<T>"
sidebar_label: "ConverterFallback<T>"
description: "Struct ConverterFallback<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct ConverterFallback\<T\> {#Aspid_MVVM_StarterKit_ConverterFallback_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What a converter does with a value it cannot convert, and what it returns instead.

```csharp
[Serializable]
public struct ConverterFallback<T>
```

#### Type Parameters

`T` 

The type the converter returns.



## Constructors

### ConverterFallback\(T, ConverterFailureMode\) {#Aspid_MVVM_StarterKit_ConverterFallback_1__ctor__0_Aspid_MVVM_StarterKit_ConverterFailureMode_}

```csharp
public ConverterFallback(T value, ConverterFailureMode mode = ConverterFailureMode.ReturnFallback)
```

#### Parameters

`value` T

Returned instead of a value that will not convert.

`mode` [ConverterFailureMode](Aspid.MVVM.StarterKit.ConverterFailureMode.md)

What to do with a value that will not convert. [`ConverterFailureMode.ReturnInput`](Aspid.MVVM.StarterKit.ConverterFailureMode.md)
passes it through when it fits the output type, and otherwise uses the fallback.

## Properties

### FallbackValue {#Aspid_MVVM_StarterKit_ConverterFallback_1_FallbackValue}

Gets the value returned instead of one that will not convert.

```csharp
public readonly T FallbackValue { get; }
```

#### Property Value

 T

### Mode {#Aspid_MVVM_StarterKit_ConverterFallback_1_Mode}

Gets what the converter does with a value it cannot convert.

```csharp
public readonly ConverterFailureMode Mode { get; }
```

#### Property Value

 [ConverterFailureMode](Aspid.MVVM.StarterKit.ConverterFailureMode.md)

## Methods

### Fail\(IConverter, object?, string\) {#Aspid_MVVM_StarterKit_ConverterFallback_1_Fail_Aspid_MVVM_StarterKit_IConverter_System_Object_System_String_}

Reports the failure and returns what [`ConverterFallback<T>.Mode`](Aspid.MVVM.StarterKit.ConverterFallback-1.md#Aspid_MVVM_StarterKit_ConverterFallback_1_Mode) says to.

```csharp
public readonly T Fail(IConverter converter, object? value, string problem)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter.md)

The failing converter — pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/this">this</a>.

`value` [object](https://learn.microsoft.com/dotnet/api/system.object)?

The value that would not convert.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

#### Returns

 T

The value itself when [`ConverterFallback<T>.Mode`](Aspid.MVVM.StarterKit.ConverterFallback-1.md#Aspid_MVVM_StarterKit_ConverterFallback_1_Mode) is [`ConverterFailureMode.ReturnInput`](Aspid.MVVM.StarterKit.ConverterFailureMode.md)
and the value already is a <code class="typeparamref">T</code>; otherwise, [`ConverterFallback<T>.FallbackValue`](Aspid.MVVM.StarterKit.ConverterFallback-1.md#Aspid_MVVM_StarterKit_ConverterFallback_1_FallbackValue).

## Operators

### implicit operator ConverterFallback\<T\>\(T\) {#Aspid_MVVM_StarterKit_ConverterFallback_1_op_Implicit__0__Aspid_MVVM_StarterKit_ConverterFallback__0_}

Wraps the specified value as a fallback with [`ConverterFailureMode.ReturnFallback`](Aspid.MVVM.StarterKit.ConverterFailureMode.md).

```csharp
public static implicit operator ConverterFallback<T>(T value)
```

#### Parameters

`value` T

Returned instead of a value that will not convert.

#### Returns

 [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<T\>

The fallback.

