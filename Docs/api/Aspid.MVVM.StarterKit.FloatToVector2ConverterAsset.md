---
title: "Class FloatToVector2ConverterAsset"
sidebar_label: "FloatToVector2ConverterAsset"
description: "Class FloatToVector2ConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class FloatToVector2ConverterAsset {#Aspid_MVVM_StarterKit_FloatToVector2ConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Single`](https://learn.microsoft.com/dotnet/api/system.single) to [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Float To Vector2 Converter", fileName = "FloatToVector2Converter")]
public sealed class FloatToVector2ConverterAsset : ConverterAsset<float, Vector2>, IConverter<float, Vector2>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<float, Vector2\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[FloatToVector2ConverterAsset](Aspid.MVVM.StarterKit.FloatToVector2ConverterAsset.md)

#### Implements

[IConverter\<float, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

