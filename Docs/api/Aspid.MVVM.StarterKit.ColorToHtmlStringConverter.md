---
title: "Class ColorToHtmlStringConverter"
sidebar_label: "ColorToHtmlStringConverter"
description: "Class ColorToHtmlStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorToHtmlStringConverter {#Aspid_MVVM_StarterKit_ColorToHtmlStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes a color as an HTML string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color/To String", Name = "To Html String", Tooltip = "Writes a color as an HTML string")]
public sealed class ColorToHtmlStringConverter : ITwoWayConverter<Color, string?>, IConverter<Color, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorToHtmlStringConverter](Aspid.MVVM.StarterKit.ColorToHtmlStringConverter.md)

#### Implements

[ITwoWayConverter\<Color, string?\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Color, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Each channel is clamped to 0..1 and rounded to a byte, so an HDR color writes as white.

## Constructors

### ColorToHtmlStringConverter\(\) {#Aspid_MVVM_StarterKit_ColorToHtmlStringConverter__ctor}

```csharp
public ColorToHtmlStringConverter()
```

#### Remarks

Default: <code>#RRGGBB</code> in upper case, with fully transparent black for a string that does
not parse.

### ColorToHtmlStringConverter\(bool, bool, bool, Color?\) {#Aspid_MVVM_StarterKit_ColorToHtmlStringConverter__ctor_System_Boolean_System_Boolean_System_Boolean_System_Nullable_UnityEngine_Color__}

```csharp
public ColorToHtmlStringConverter(bool includeAlpha, bool includeHash = true, bool lowercase = false, Color? convertBackFallback = null)
```

#### Parameters

`includeAlpha` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to include the alpha channel.

`includeHash` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to prefix the string with a hash. A string written without it does not parse back.

`lowercase` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to write the digits in lower case.

`convertBackFallback` Color?

Returned when the string coming back is blank or does not parse. When omitted, fully
transparent black.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorToHtmlStringConverter_Convert_UnityEngine_Color_}

Writes the specified color as an HTML string.

```csharp
public string Convert(Color value)
```

#### Parameters

`value` Color

The color to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

<code>RRGGBB</code>, with the alpha pair and the leading hash as configured.

### ConvertBack\(string?\) {#Aspid_MVVM_StarterKit_ColorToHtmlStringConverter_ConvertBack_System_String_}

Parses an HTML string coming back from the View.

```csharp
public Color ConvertBack(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The HTML color string (e.g., "#FF0000").

#### Returns

 Color

The parsed color, or the fallback. A blank string is treated as no value rather than as a
failed parse and returns the fallback silently. A string written without the leading hash
does not parse back.

