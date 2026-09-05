---
title: "Class RomanNumeralConverter"
sidebar_label: "RomanNumeralConverter"
description: "Class RomanNumeralConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RomanNumeralConverter {#Aspid_MVVM_StarterKit_RomanNumeralConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a number as a Roman numeral.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Roman Numeral", Tooltip = "Formats a number as a Roman numeral")]
public sealed class RomanNumeralConverter : IConverter<int, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RomanNumeralConverter](Aspid.MVVM.StarterKit.RomanNumeralConverter.md)

#### Implements

[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### RomanNumeralConverter\(\) {#Aspid_MVVM_StarterKit_RomanNumeralConverter__ctor}

```csharp
public RomanNumeralConverter()
```

#### Remarks

Default: upper case.

### RomanNumeralConverter\(bool\) {#Aspid_MVVM_StarterKit_RomanNumeralConverter__ctor_System_Boolean_}

```csharp
public RomanNumeralConverter(bool lowercase)
```

#### Parameters

`lowercase` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, writes the numeral in lower case.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_RomanNumeralConverter_Convert_System_Int32_}

Formats the specified number as a Roman numeral.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The numeral, or the number in digits when it is outside 1..3999.

