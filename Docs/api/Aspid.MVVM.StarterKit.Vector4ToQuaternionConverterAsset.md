---
title: "Class Vector4ToQuaternionConverterAsset"
sidebar_label: "Vector4ToQuaternionConverterAsset"
description: "Class Vector4ToQuaternionConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector4ToQuaternionConverterAsset {#Aspid_MVVM_StarterKit_Vector4ToQuaternionConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) to [`Quaternion`](https://docs.unity3d.com/ScriptReference/Quaternion.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Vector/Vector4 To Quaternion Converter", fileName = "Vector4ToQuaternionConverter")]
public sealed class Vector4ToQuaternionConverterAsset : ConverterAsset<Vector4, Quaternion>, IConverter<Vector4, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Vector4, Quaternion\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[Vector4ToQuaternionConverterAsset](Aspid.MVVM.StarterKit.Vector4ToQuaternionConverterAsset.md)

#### Implements

[IConverter\<Vector4, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

