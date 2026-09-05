---
title: "Class StringMatchToBoolConverter"
sidebar_label: "StringMatchToBoolConverter"
description: "Class StringMatchToBoolConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringMatchToBoolConverter {#Aspid_MVVM_StarterKit_StringMatchToBoolConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Tests a bound string against an authored one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Bool", Name = "Match", Tooltip = "Tests a bound string against an authored one")]
public sealed class StringMatchToBoolConverter : IConverter<string?, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringMatchToBoolConverter](Aspid.MVVM.StarterKit.StringMatchToBoolConverter.md)

#### Implements

[IConverter\<string?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### StringMatchToBoolConverter\(StringMatchMode, string, bool, bool\) {#Aspid_MVVM_StarterKit_StringMatchToBoolConverter__ctor_Aspid_MVVM_StarterKit_StringMatchMode_System_String_System_Boolean_System_Boolean_}

```csharp
public StringMatchToBoolConverter(StringMatchMode mode, string text, bool ignoreCase = true, bool isInvert = false)
```

#### Parameters

`mode` [StringMatchMode](Aspid.MVVM.StarterKit.StringMatchMode.md)

How the bound string is compared with <code class="paramref">text</code>.

`text` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text the bound string is compared against. Blank is reported and answers <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

`ignoreCase` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, compares without regard to case.

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringMatchToBoolConverter_Convert_System_String_}

Tests the specified string against the authored text.

```csharp
public bool Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to test. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> matches nothing.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result, inverted when configured. Blank text or an undeclared mode reports an error and returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

