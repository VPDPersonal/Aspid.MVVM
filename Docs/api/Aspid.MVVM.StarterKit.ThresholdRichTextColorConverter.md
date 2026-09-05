---
title: "Class ThresholdRichTextColorConverter"
sidebar_label: "ThresholdRichTextColorConverter"
description: "Class ThresholdRichTextColorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ThresholdRichTextColorConverter {#Aspid_MVVM_StarterKit_ThresholdRichTextColorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes a number as colored text, the color chosen by how large it is.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Threshold Rich Text Color", Tooltip = "Writes a number as colored text, the color chosen by how large it is")]
public sealed class ThresholdRichTextColorConverter : IConverter<float, string>, IConverter<int, string>, IConverter<long, string>, IConverter<double, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ThresholdRichTextColorConverter](Aspid.MVVM.StarterKit.ThresholdRichTextColorConverter.md)

#### Implements

[IConverter\<float, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ThresholdRichTextColorConverter\(ColorStop\[\]?, Color, IConverter\<float, string?\>?\) {#Aspid_MVVM_StarterKit_ThresholdRichTextColorConverter__ctor_Aspid_MVVM_StarterKit_ColorStop___UnityEngine_Color_Aspid_MVVM_StarterKit_IConverter_System_Single_System_String__}

```csharp
public ThresholdRichTextColorConverter(ColorStop[]? stops, Color fallback, IConverter<float, string?>? number = null)
```

#### Parameters

`stops` [ColorStop](Aspid.MVVM.StarterKit.ColorStop.md)\[\]?

Colors by threshold. With none the converter has nothing to pick from, which is reported
as an error.

`fallback` Color

Used when the value is below every threshold.

`number` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[float](https://learn.microsoft.com/dotnet/api/system.single), [string](https://learn.microsoft.com/dotnet/api/system.string)?\>?

Writes the number itself. When omitted, writes it as <code>0.##</code> in the device locale.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_ThresholdRichTextColorConverter_Convert_System_Single_}

Writes the specified number as colored text.

```csharp
public string Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The tagged number.

