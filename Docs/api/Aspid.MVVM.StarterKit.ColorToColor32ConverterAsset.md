---
title: "Class ColorToColor32ConverterAsset"
sidebar_label: "ColorToColor32ConverterAsset"
description: "Class ColorToColor32ConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorToColor32ConverterAsset {#Aspid_MVVM_StarterKit_ColorToColor32ConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) to [`Color32`](https://docs.unity3d.com/ScriptReference/Color32.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color To Color32 Converter", fileName = "ColorToColor32Converter")]
public sealed class ColorToColor32ConverterAsset : ConverterAsset<Color, Color32>, IConverter<Color, Color32>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Color, Color32\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[ColorToColor32ConverterAsset](Aspid.MVVM.StarterKit.ColorToColor32ConverterAsset.md)

#### Implements

[IConverter\<Color, Color32\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

