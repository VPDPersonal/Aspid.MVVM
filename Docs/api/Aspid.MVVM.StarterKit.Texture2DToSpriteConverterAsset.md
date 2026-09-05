---
title: "Class Texture2DToSpriteConverterAsset"
sidebar_label: "Texture2DToSpriteConverterAsset"
description: "Class Texture2DToSpriteConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Texture2DToSpriteConverterAsset {#Aspid_MVVM_StarterKit_Texture2DToSpriteConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html) to [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Texture/Texture2D To Sprite Converter", fileName = "Texture2DToSpriteConverter")]
public sealed class Texture2DToSpriteConverterAsset : ConverterAsset<Texture2D?, Sprite?>, IConverter<Texture2D?, Sprite?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Texture2D?, Sprite?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[Texture2DToSpriteConverterAsset](Aspid.MVVM.StarterKit.Texture2DToSpriteConverterAsset.md)

#### Implements

[IConverter\<Texture2D?, Sprite?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

