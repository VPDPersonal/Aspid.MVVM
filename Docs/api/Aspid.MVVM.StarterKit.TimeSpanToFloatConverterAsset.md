---
title: "Class TimeSpanToFloatConverterAsset"
sidebar_label: "TimeSpanToFloatConverterAsset"
description: "Class TimeSpanToFloatConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TimeSpanToFloatConverterAsset {#Aspid_MVVM_StarterKit_TimeSpanToFloatConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) to [`Single`](https://learn.microsoft.com/dotnet/api/system.single) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Time Span To Float Converter", fileName = "TimeSpanToFloatConverter")]
public sealed class TimeSpanToFloatConverterAsset : ConverterAsset<TimeSpan, float>, IConverter<TimeSpan, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<TimeSpan, float\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[TimeSpanToFloatConverterAsset](Aspid.MVVM.StarterKit.TimeSpanToFloatConverterAsset.md)

#### Implements

[IConverter\<TimeSpan, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

