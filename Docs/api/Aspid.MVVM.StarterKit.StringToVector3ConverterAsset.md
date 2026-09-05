---
title: "Class StringToVector3ConverterAsset"
sidebar_label: "StringToVector3ConverterAsset"
description: "Class StringToVector3ConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToVector3ConverterAsset {#Aspid_MVVM_StarterKit_StringToVector3ConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`String`](https://learn.microsoft.com/dotnet/api/system.string) to [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/String/String To Vector3 Converter", fileName = "StringToVector3Converter")]
public sealed class StringToVector3ConverterAsset : ConverterAsset<string?, Vector3>, IConverter<string?, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<string?, Vector3\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[StringToVector3ConverterAsset](Aspid.MVVM.StarterKit.StringToVector3ConverterAsset.md)

#### Implements

[IConverter\<string?, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

