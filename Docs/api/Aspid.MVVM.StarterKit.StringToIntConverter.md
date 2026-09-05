---
title: "Class StringToIntConverter"
sidebar_label: "StringToIntConverter"
description: "Class StringToIntConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToIntConverter {#Aspid_MVVM_StarterKit_StringToIntConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a whole number out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Number", Name = "Parse Int", Tooltip = "Reads a whole number out of text")]
public sealed class StringToIntConverter : StringToNumberConverter<int>, ITwoWayConverter<string?, int>, IConverter<string?, int>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToNumberConverter\<int\>](Aspid.MVVM.StarterKit.StringToNumberConverter-1.md) ← 
[StringToIntConverter](Aspid.MVVM.StarterKit.StringToIntConverter.md)

#### Implements

[ITwoWayConverter\<string?, int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The culture decides the group separator: <code>1.000</code> is a thousand in one culture and nothing in another.

## Constructors

### StringToIntConverter\(\) {#Aspid_MVVM_StarterKit_StringToIntConverter__ctor}

```csharp
public StringToIntConverter()
```

#### Remarks

Default: falling back to zero.

### StringToIntConverter\(int, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToIntConverter__ctor_System_Int32_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToIntConverter(int fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`fallback` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Returned when the text is not a number.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is read with.

## Properties

### Expected {#Aspid_MVVM_StarterKit_StringToIntConverter_Expected}

Gets what the text was expected to be, as a noun phrase: "a whole number".

```csharp
protected override string Expected { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Clamp\(int, int, int\) {#Aspid_MVVM_StarterKit_StringToIntConverter_Clamp_System_Int32_System_Int32_System_Int32_}

Holds the number inside the bounds.

```csharp
protected override int Clamp(int value, int min, int max)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to hold.

`min` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The lowest value allowed through.

`max` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The highest value allowed through.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number, or the bound it fell outside.

### ConvertBack\(int\) {#Aspid_MVVM_StarterKit_StringToIntConverter_ConvertBack_System_Int32_}

Writes the specified number as text.

```csharp
public override string ConvertBack(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The text.

### TryParse\(string?, CultureInfo, out int\) {#Aspid_MVVM_StarterKit_StringToIntConverter_TryParse_System_String_System_Globalization_CultureInfo_System_Int32__}

Reads the number the specified text is written as.

```csharp
protected override bool TryParse(string? value, CultureInfo culture, out int result)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

`culture` [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

The culture the text is written in.

`result` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number read, or the type default.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the text is a number; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

