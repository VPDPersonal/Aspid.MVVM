---
title: "Class StringToNumberConverter<T>"
sidebar_label: "StringToNumberConverter<T>"
description: "Class StringToNumberConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToNumberConverter\<T\> {#Aspid_MVVM_StarterKit_StringToNumberConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base for a converter that reads a number out of text and writes it back.

```csharp
[Serializable]
public abstract class StringToNumberConverter<T> : ITwoWayConverter<string?, T>, IConverter<string?, T>, IConverter where T : struct
```

#### Type Parameters

`T` 

The numeric type being read.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToNumberConverter\<T\>](Aspid.MVVM.StarterKit.StringToNumberConverter-1.md)

#### Implements

[ITwoWayConverter\<string?, T\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, T\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The clamp sits after the parse, so a fallback authored outside the bounds stays outside.

## Constructors

### StringToNumberConverter\(T, T\) {#Aspid_MVVM_StarterKit_StringToNumberConverter_1__ctor__0__0_}

```csharp
protected StringToNumberConverter(T min, T max)
```

#### Parameters

`min` T

The lowest value the type holds; the default lower bound.

`max` T

The highest value the type holds; the default upper bound.

### StringToNumberConverter\(T, T, T, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToNumberConverter_1__ctor__0__0__0_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
protected StringToNumberConverter(T min, T max, T fallback, CultureInfoMode culture)
```

#### Parameters

`min` T

The lowest value the type holds; the default lower bound.

`max` T

The highest value the type holds; the default upper bound.

`fallback` T

Returned when the text is not a number.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is read with.

## Properties

### Culture {#Aspid_MVVM_StarterKit_StringToNumberConverter_1_Culture}

Gets the culture the text is read and written with.

```csharp
protected CultureInfo Culture { get; }
```

#### Property Value

 [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

### Expected {#Aspid_MVVM_StarterKit_StringToNumberConverter_1_Expected}

Gets what the text was expected to be, as a noun phrase: "a whole number".

```csharp
protected abstract string Expected { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Clamp\(T, T, T\) {#Aspid_MVVM_StarterKit_StringToNumberConverter_1_Clamp__0__0__0_}

Holds the number inside the bounds.

```csharp
protected abstract T Clamp(T value, T min, T max)
```

#### Parameters

`value` T

The number to hold.

`min` T

The lowest value allowed through.

`max` T

The highest value allowed through.

#### Returns

 T

The number, or the bound it fell outside.

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToNumberConverter_1_Convert_System_String_}

Reads a number out of the specified text.

```csharp
public T Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 T

The number, held inside the bounds when clamping, or the fallback when the text is not one.

### ConvertBack\(T\) {#Aspid_MVVM_StarterKit_StringToNumberConverter_1_ConvertBack__0_}

Writes the specified number as text.

```csharp
public abstract string ConvertBack(T value)
```

#### Parameters

`value` T

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The text.

### TryParse\(string?, CultureInfo, out T\) {#Aspid_MVVM_StarterKit_StringToNumberConverter_1_TryParse_System_String_System_Globalization_CultureInfo__0__}

Reads the number the specified text is written as.

```csharp
protected abstract bool TryParse(string? value, CultureInfo culture, out T result)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

`culture` [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

The culture the text is written in.

`result` T

The number read, or the type default.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the text is a number; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

