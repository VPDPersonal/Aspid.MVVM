---
title: "Class AbbreviatedNumberConverter"
sidebar_label: "AbbreviatedNumberConverter"
description: "Class AbbreviatedNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AbbreviatedNumberConverter {#Aspid_MVVM_StarterKit_AbbreviatedNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Abbreviated", Tooltip = "Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M")]
public sealed class AbbreviatedNumberConverter : IConverter<double, string>, IConverter<int, string>, IConverter<long, string>, IConverter<float, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AbbreviatedNumberConverter](Aspid.MVVM.StarterKit.AbbreviatedNumberConverter.md)

#### Implements

[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### AbbreviatedNumberConverter\(\) {#Aspid_MVVM_StarterKit_AbbreviatedNumberConverter__ctor}

```csharp
public AbbreviatedNumberConverter()
```

#### Remarks

Default: with K/M/B/T suffixes.

### AbbreviatedNumberConverter\(int, string\[\]?\) {#Aspid_MVVM_StarterKit_AbbreviatedNumberConverter__ctor_System_Int32_System_String___}

```csharp
public AbbreviatedNumberConverter(int decimals, string[]? suffixes = null)
```

#### Parameters

`decimals` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many decimals to show, shortened or in full.

`suffixes` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

The suffix for each power of a thousand, starting with none. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> keeps the defaults.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">decimals</code> is negative.

## Methods

### Convert\(double\) {#Aspid_MVVM_StarterKit_AbbreviatedNumberConverter_Convert_System_Double_}

Shortens the specified number.

```csharp
public string Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to shorten.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The shortened number with its suffix, or the number in full below the threshold or without suffixes.

