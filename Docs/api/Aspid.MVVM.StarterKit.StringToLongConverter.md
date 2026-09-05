---
title: "Class StringToLongConverter"
sidebar_label: "StringToLongConverter"
description: "Class StringToLongConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToLongConverter {#Aspid_MVVM_StarterKit_StringToLongConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a whole number out of text, past the range an <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">int</a> can hold.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Number", Name = "Parse Long", Tooltip = "Reads a whole number out of text, past the range an int can hold")]
public sealed class StringToLongConverter : StringToNumberConverter<long>, ITwoWayConverter<string?, long>, IConverter<string?, long>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToNumberConverter\<long\>](Aspid.MVVM.StarterKit.StringToNumberConverter-1.md) ← 
[StringToLongConverter](Aspid.MVVM.StarterKit.StringToLongConverter.md)

#### Implements

[ITwoWayConverter\<string?, long\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The culture decides the group separator: <code>1.000</code> is a thousand in one culture and nothing in another.

## Constructors

### StringToLongConverter\(\) {#Aspid_MVVM_StarterKit_StringToLongConverter__ctor}

```csharp
public StringToLongConverter()
```

#### Remarks

Default: falling back to zero.

### StringToLongConverter\(long, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToLongConverter__ctor_System_Int64_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToLongConverter(long fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`fallback` [long](https://learn.microsoft.com/dotnet/api/system.int64)

Returned when the text is not a number.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is read with.

## Properties

### Expected {#Aspid_MVVM_StarterKit_StringToLongConverter_Expected}

Gets what the text was expected to be, as a noun phrase: "a whole number".

```csharp
protected override string Expected { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Clamp\(long, long, long\) {#Aspid_MVVM_StarterKit_StringToLongConverter_Clamp_System_Int64_System_Int64_System_Int64_}

Holds the number inside the bounds.

```csharp
protected override long Clamp(long value, long min, long max)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number to hold.

`min` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The lowest value allowed through.

`max` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The highest value allowed through.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number, or the bound it fell outside.

### ConvertBack\(long\) {#Aspid_MVVM_StarterKit_StringToLongConverter_ConvertBack_System_Int64_}

Writes the specified number as text.

```csharp
public override string ConvertBack(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The text.

### TryParse\(string?, CultureInfo, out long\) {#Aspid_MVVM_StarterKit_StringToLongConverter_TryParse_System_String_System_Globalization_CultureInfo_System_Int64__}

Reads the number the specified text is written as.

```csharp
protected override bool TryParse(string? value, CultureInfo culture, out long result)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

`culture` [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

The culture the text is written in.

`result` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number read, or the type default.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the text is a number; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

