---
title: "Class ColorToVector4ConverterAsset"
sidebar_label: "ColorToVector4ConverterAsset"
description: "Class ColorToVector4ConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorToVector4ConverterAsset {#Aspid_MVVM_StarterKit_ColorToVector4ConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) to [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color To Vector4 Converter", fileName = "ColorToVector4Converter")]
public sealed class ColorToVector4ConverterAsset : ConverterAsset<Color, Vector4>, IConverter<Color, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Color, Vector4\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[ColorToVector4ConverterAsset](Aspid.MVVM.StarterKit.ColorToVector4ConverterAsset.md)

#### Implements

[IConverter\<Color, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

