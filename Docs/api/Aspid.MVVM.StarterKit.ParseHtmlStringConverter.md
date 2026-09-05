---
title: "Class ParseHtmlStringConverter"
sidebar_label: "ParseHtmlStringConverter"
description: "Class ParseHtmlStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ParseHtmlStringConverter {#Aspid_MVVM_StarterKit_ParseHtmlStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts HTML color strings (e.g., "#FF0000") to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) values.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Color", Name = "Parse Html Color", Tooltip = "Converts HTML color strings (e.g., '#FF0000') to Color values")]
public sealed class ParseHtmlStringConverter : ITwoWayConverter<string?, Color>, IConverter<string?, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ParseHtmlStringConverter](Aspid.MVVM.StarterKit.ParseHtmlStringConverter.md)

#### Implements

[ITwoWayConverter\<string?, Color\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The default fallback is fully transparent black, also what <code>"#00000000"</code> parses to,
so a failure is reported every time rather than inferred from the color.

## Constructors

### ParseHtmlStringConverter\(\) {#Aspid_MVVM_StarterKit_ParseHtmlStringConverter__ctor}

```csharp
public ParseHtmlStringConverter()
```

#### Remarks

Default: fully transparent black for a string that does not parse.

### ParseHtmlStringConverter\(Color?\) {#Aspid_MVVM_StarterKit_ParseHtmlStringConverter__ctor_System_Nullable_UnityEngine_Color__}

```csharp
public ParseHtmlStringConverter(Color? fallback = null)
```

#### Parameters

`fallback` Color?

Returned when the string is blank or does not parse. When omitted, fully transparent black.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_ParseHtmlStringConverter_Convert_System_String_}

Converts an HTML color string to a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html).

```csharp
public Color Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The HTML color string (e.g., "#FF0000").

#### Returns

 Color

The parsed color, or the fallback. A blank string is treated as no value rather than as a
failed parse and returns the fallback silently.

### ConvertBack\(Color\) {#Aspid_MVVM_StarterKit_ParseHtmlStringConverter_ConvertBack_UnityEngine_Color_}

Writes the specified color as an HTML color string.

```csharp
public string ConvertBack(Color value)
```

#### Parameters

`value` Color

The color to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

<code>#RRGGBBAA</code>. The alpha pair is always written, so the string parses back to the color
it came from; an HDR channel is clamped to 0..1 and rounded, and does not survive the trip.

