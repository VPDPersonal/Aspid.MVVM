---
title: "Class StringToDoubleConverter"
sidebar_label: "StringToDoubleConverter"
description: "Class StringToDoubleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToDoubleConverter {#Aspid_MVVM_StarterKit_StringToDoubleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a decimal number out of text, keeping the precision a float would lose.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Number", Name = "Parse Double", Tooltip = "Reads a decimal number out of text, keeping the precision a float would lose")]
public sealed class StringToDoubleConverter : StringToNumberConverter<double>, ITwoWayConverter<string?, double>, IConverter<string?, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToNumberConverter\<double\>](Aspid.MVVM.StarterKit.StringToNumberConverter-1.md) ← 
[StringToDoubleConverter](Aspid.MVVM.StarterKit.StringToDoubleConverter.md)

#### Implements

[ITwoWayConverter\<string?, double\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The culture decides what a comma means: <code>1,5</code> is one and a half in German and fifteen in invariant.

## Constructors

### StringToDoubleConverter\(\) {#Aspid_MVVM_StarterKit_StringToDoubleConverter__ctor}

```csharp
public StringToDoubleConverter()
```

#### Remarks

Default: falling back to zero.

### StringToDoubleConverter\(double, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToDoubleConverter__ctor_System_Double_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToDoubleConverter(double fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`fallback` [double](https://learn.microsoft.com/dotnet/api/system.double)

Returned when the text is not a number.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is read with.

## Properties

### Expected {#Aspid_MVVM_StarterKit_StringToDoubleConverter_Expected}

Gets what the text was expected to be, as a noun phrase: "a whole number".

```csharp
protected override string Expected { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Clamp\(double, double, double\) {#Aspid_MVVM_StarterKit_StringToDoubleConverter_Clamp_System_Double_System_Double_System_Double_}

Holds the number inside the bounds.

```csharp
protected override double Clamp(double value, double min, double max)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to hold.

`min` [double](https://learn.microsoft.com/dotnet/api/system.double)

The lowest value allowed through.

`max` [double](https://learn.microsoft.com/dotnet/api/system.double)

The highest value allowed through.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number, or the bound it fell outside.

### ConvertBack\(double\) {#Aspid_MVVM_StarterKit_StringToDoubleConverter_ConvertBack_System_Double_}

Writes the specified number as text, in the round-trip format.

```csharp
public override string ConvertBack(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The text.

### TryParse\(string?, CultureInfo, out double\) {#Aspid_MVVM_StarterKit_StringToDoubleConverter_TryParse_System_String_System_Globalization_CultureInfo_System_Double__}

Reads the number the specified text is written as.

```csharp
protected override bool TryParse(string? value, CultureInfo culture, out double result)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

`culture` [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

The culture the text is written in.

`result` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number read, or the type default.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the text is a number; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

