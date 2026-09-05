---
title: "Class Vector2ToFloatConverterAsset"
sidebar_label: "Vector2ToFloatConverterAsset"
description: "Class Vector2ToFloatConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector2ToFloatConverterAsset {#Aspid_MVVM_StarterKit_Vector2ToFloatConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector2 To Float Converter", fileName = "Vector2ToFloatConverter")]
public sealed class Vector2ToFloatConverterAsset : ConverterAsset<Vector2, float>, IConverter<Vector2, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Vector2, float\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[Vector2ToFloatConverterAsset](Aspid.MVVM.StarterKit.Vector2ToFloatConverterAsset.md)

#### Implements

[IConverter\<Vector2, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

