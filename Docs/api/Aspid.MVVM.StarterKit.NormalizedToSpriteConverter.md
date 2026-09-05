---
title: "Class NormalizedToSpriteConverter"
sidebar_label: "NormalizedToSpriteConverter"
description: "Class NormalizedToSpriteConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NormalizedToSpriteConverter {#Aspid_MVVM_StarterKit_NormalizedToSpriteConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Picks one of a list of sprites by a 0..1 amount.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Sprite", Name = "Normalized To Sprite", Tooltip = "Picks one of a list of sprites by a 0..1 amount")]
public sealed class NormalizedToSpriteConverter : IConverter<float, Sprite?>, IConverter<double, Sprite?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NormalizedToSpriteConverter](Aspid.MVVM.StarterKit.NormalizedToSpriteConverter.md)

#### Implements

[IConverter\<float, Sprite?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, Sprite?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### NormalizedToSpriteConverter\(Sprite\[\]?\) {#Aspid_MVVM_StarterKit_NormalizedToSpriteConverter__ctor_UnityEngine_Sprite___}

```csharp
public NormalizedToSpriteConverter(Sprite[]? frames)
```

#### Parameters

`frames` Sprite\[\]?

The frames, from empty to full. With none the converter has nothing to pick from, which is
reported as an error.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_NormalizedToSpriteConverter_Convert_System_Single_}

Picks the frame for the specified amount.

```csharp
public Sprite? Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 amount.

#### Returns

 Sprite?

The frame at that amount. With no frames authored the failure is reported as an error and
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is returned.

