---
title: "Class StringToDecimalConverter"
sidebar_label: "StringToDecimalConverter"
description: "Class StringToDecimalConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToDecimalConverter {#Aspid_MVVM_StarterKit_StringToDecimalConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads an exact decimal number out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Number", Name = "Parse Decimal", Tooltip = "Reads an exact decimal number out of text")]
public sealed class StringToDecimalConverter : ITwoWayConverter<string?, decimal>, IConverter<string?, decimal>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToDecimalConverter](Aspid.MVVM.StarterKit.StringToDecimalConverter.md)

#### Implements

[ITwoWayConverter\<string?, decimal\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, decimal\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Unity cannot serialize a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">decimal</a>, so the fallback and bounds are text in the invariant culture: <code>1.5</code>, never <code>1,5</code>.

## Constructors

### StringToDecimalConverter\(\) {#Aspid_MVVM_StarterKit_StringToDecimalConverter__ctor}

```csharp
public StringToDecimalConverter()
```

#### Remarks

Default: falling back to zero.

### StringToDecimalConverter\(decimal, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringToDecimalConverter__ctor_System_Decimal_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringToDecimalConverter(decimal fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`fallback` [decimal](https://learn.microsoft.com/dotnet/api/system.decimal)

Returned when the text is not a number.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the text is read with.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToDecimalConverter_Convert_System_String_}

Reads a number out of the specified text.

```csharp
public decimal Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 [decimal](https://learn.microsoft.com/dotnet/api/system.decimal)

The number, held inside the bounds when clamping, or the fallback when the text is not one.

### ConvertBack\(decimal\) {#Aspid_MVVM_StarterKit_StringToDecimalConverter_ConvertBack_System_Decimal_}

Writes the specified number as text.

```csharp
public string ConvertBack(decimal value)
```

#### Parameters

`value` [decimal](https://learn.microsoft.com/dotnet/api/system.decimal)

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The text.

