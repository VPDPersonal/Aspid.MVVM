---
title: "Class Color32ToColorConverterAsset"
sidebar_label: "Color32ToColorConverterAsset"
description: "Class Color32ToColorConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Color32ToColorConverterAsset {#Aspid_MVVM_StarterKit_Color32ToColorConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Color32`](https://docs.unity3d.com/ScriptReference/Color32.html) to [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Color/Color32 To Color Converter", fileName = "Color32ToColorConverter")]
public sealed class Color32ToColorConverterAsset : ConverterAsset<Color32, Color>, IConverter<Color32, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Color32, Color\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[Color32ToColorConverterAsset](Aspid.MVVM.StarterKit.Color32ToColorConverterAsset.md)

#### Implements

[IConverter\<Color32, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

