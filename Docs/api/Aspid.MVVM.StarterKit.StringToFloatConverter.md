---
title: "Class StringToFloatConverter"
sidebar_label: "StringToFloatConverter"
description: "Class StringToFloatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToFloatConverter {#Aspid_MVVM_StarterKit_StringToFloatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a decimal number out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Number", Name = "Parse Float", Tooltip = "Reads a decimal number out of text")]
public sealed class StringToFloatConverter : StringToNumberConverter<float>, ITwoWayConverter<string?, float>, IConverter<string?, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToNumberConverter\<float\>](Aspid.MVVM.StarterKit.StringToNumberConverter-1.md) ← 
[StringToFloatConverter](Aspid.MVVM.StarterKit.StringToFloatConverter.md)

#### Implements

[ITwoWayConverter\<string?, float\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The culture decides what a comma means: <code>1,5</code> is one and a half in German and fifteen in invariant.

## Constructors

### StringToFloatConverter\(\) {#Aspid_MVVM_StarterKit_StringToFloatConverter__ctor}

```csharp
public StringToFloatConverter()
```

#### Remarks

Default: falling back to zero.

### StringToFloatConverter\(float, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToFloatConverter__ctor_System_Single_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToFloatConverter(float fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`fallback` [float](https://learn.microsoft.com/dotnet/api/system.single)

Returned when the text is not a number.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is read with.

## Properties

### Expected {#Aspid_MVVM_StarterKit_StringToFloatConverter_Expected}

Gets what the text was expected to be, as a noun phrase: "a whole number".

```csharp
protected override string Expected { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### Clamp\(float, float, float\) {#Aspid_MVVM_StarterKit_StringToFloatConverter_Clamp_System_Single_System_Single_System_Single_}

Holds the number inside the bounds.

```csharp
protected override float Clamp(float value, float min, float max)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to hold.

`min` [float](https://learn.microsoft.com/dotnet/api/system.single)

The lowest value allowed through.

`max` [float](https://learn.microsoft.com/dotnet/api/system.single)

The highest value allowed through.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The number, or the bound it fell outside.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_StringToFloatConverter_ConvertBack_System_Single_}

Writes the specified number as text, in the round-trip format.

```csharp
public override string ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The text.

### TryParse\(string?, CultureInfo, out float\) {#Aspid_MVVM_StarterKit_StringToFloatConverter_TryParse_System_String_System_Globalization_CultureInfo_System_Single__}

Reads the number the specified text is written as.

```csharp
protected override bool TryParse(string? value, CultureInfo culture, out float result)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

`culture` [CultureInfo](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo)

The culture the text is written in.

`result` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number read, or the type default.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the text is a number; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

