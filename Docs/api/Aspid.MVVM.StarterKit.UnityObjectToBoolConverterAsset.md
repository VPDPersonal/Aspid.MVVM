---
title: "Class UnityObjectToBoolConverterAsset"
sidebar_label: "UnityObjectToBoolConverterAsset"
description: "Class UnityObjectToBoolConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class UnityObjectToBoolConverterAsset {#Aspid_MVVM_StarterKit_UnityObjectToBoolConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Object`](https://docs.unity3d.com/ScriptReference/Object.html) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Object/Unity Object To Bool Converter", fileName = "UnityObjectToBoolConverter")]
public sealed class UnityObjectToBoolConverterAsset : ConverterAsset<Object?, bool>, IConverter<Object?, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<Object?, bool\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[UnityObjectToBoolConverterAsset](Aspid.MVVM.StarterKit.UnityObjectToBoolConverterAsset.md)

#### Implements

[IConverter\<Object?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

