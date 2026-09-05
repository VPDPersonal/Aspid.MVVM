---
title: "Class Vector3ToVector4ConverterAsset"
sidebar_label: "Vector3ToVector4ConverterAsset"
description: "Class Vector3ToVector4ConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector3ToVector4ConverterAsset {#Aspid_MVVM_StarterKit_Vector3ToVector4ConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) to [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector3 To Vector4 Converter", fileName = "Vector3ToVector4Converter")]
public sealed class Vector3ToVector4ConverterAsset : ConverterAsset<Vector3, Vector4>, IConverter<Vector3, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Vector3, Vector4\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[Vector3ToVector4ConverterAsset](Aspid.MVVM.StarterKit.Vector3ToVector4ConverterAsset.md)

#### Implements

[IConverter\<Vector3, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

