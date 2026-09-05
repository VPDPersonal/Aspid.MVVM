---
title: "Class DoubleToBoolConverterAsset"
sidebar_label: "DoubleToBoolConverterAsset"
description: "Class DoubleToBoolConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DoubleToBoolConverterAsset {#Aspid_MVVM_StarterKit_DoubleToBoolConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`Double`](https://learn.microsoft.com/dotnet/api/system.double) to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Numbers/Double To Bool Converter", fileName = "DoubleToBoolConverter")]
public sealed class DoubleToBoolConverterAsset : ConverterAsset<double, bool>, IConverter<double, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<double, bool\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[DoubleToBoolConverterAsset](Aspid.MVVM.StarterKit.DoubleToBoolConverterAsset.md)

#### Implements

[IConverter\<double, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

