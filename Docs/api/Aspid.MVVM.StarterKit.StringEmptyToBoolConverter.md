---
title: "Class StringEmptyToBoolConverter"
sidebar_label: "StringEmptyToBoolConverter"
description: "Class StringEmptyToBoolConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringEmptyToBoolConverter {#Aspid_MVVM_StarterKit_StringEmptyToBoolConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Tests whether a string is absent.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Bool", Name = "Is Empty", Tooltip = "Tests whether a string is absent")]
public sealed class StringEmptyToBoolConverter : IConverter<string?, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringEmptyToBoolConverter](Aspid.MVVM.StarterKit.StringEmptyToBoolConverter.md)

#### Implements

[IConverter\<string?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for an absent string; invert it to drive <code>SetActive</code>.

## Constructors

### StringEmptyToBoolConverter\(\) {#Aspid_MVVM_StarterKit_StringEmptyToBoolConverter__ctor}

```csharp
public StringEmptyToBoolConverter()
```

#### Remarks

Default: <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or empty string.

### StringEmptyToBoolConverter\(bool\) {#Aspid_MVVM_StarterKit_StringEmptyToBoolConverter__ctor_System_Boolean_}

```csharp
public StringEmptyToBoolConverter(bool isInvert)
```

#### Parameters

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

### StringEmptyToBoolConverter\(StringEmptiness, bool\) {#Aspid_MVVM_StarterKit_StringEmptyToBoolConverter__ctor_Aspid_MVVM_StarterKit_StringEmptiness_System_Boolean_}

```csharp
public StringEmptyToBoolConverter(StringEmptiness emptiness, bool isInvert = false)
```

#### Parameters

`emptiness` [StringEmptiness](Aspid.MVVM.StarterKit.StringEmptiness.md)

What counts as an absent string.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringEmptyToBoolConverter_Convert_System_String_}

Tests whether the specified string is absent under the configured [`StringEmptiness`](Aspid.MVVM.StarterKit.StringEmptiness.md).

```csharp
public bool Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when the string is absent, inverted when configured. An undeclared emptiness reports an error and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

