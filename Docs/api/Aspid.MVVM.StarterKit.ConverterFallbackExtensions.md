---
title: "Class ConverterFallbackExtensions"
sidebar_label: "ConverterFallbackExtensions"
description: "Class ConverterFallbackExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConverterFallbackExtensions {#Aspid_MVVM_StarterKit_ConverterFallbackExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reports a failure and hands back the fallback in one call.

```csharp
public static class ConverterFallbackExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConverterFallbackExtensions](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md)



## Methods

### UseFallback\<T\>\(IConverter, T, string\) {#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_}

Reports the failure and returns the specified fallback.

```csharp
public static T UseFallback<T>(this IConverter converter, T fallback, string problem)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter.md)

The failing converter — pass <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/this">this</a>.

`fallback` T

Returned instead of the value that would not convert.

`problem` [string](https://learn.microsoft.com/dotnet/api/system.string)

What is wrong, as a sentence without the trailing period.

#### Returns

 T

<code class="paramref">fallback</code>.

#### Type Parameters

`T` 

The type the converter returns.

