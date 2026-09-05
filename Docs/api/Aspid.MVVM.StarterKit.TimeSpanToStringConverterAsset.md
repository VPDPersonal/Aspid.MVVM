---
title: "Class TimeSpanToStringConverterAsset"
sidebar_label: "TimeSpanToStringConverterAsset"
description: "Class TimeSpanToStringConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TimeSpanToStringConverterAsset {#Aspid_MVVM_StarterKit_TimeSpanToStringConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) to [`String`](https://learn.microsoft.com/dotnet/api/system.string) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Time Span To String Converter", fileName = "TimeSpanToStringConverter")]
public sealed class TimeSpanToStringConverterAsset : ConverterAsset<TimeSpan, string?>, IConverter<TimeSpan, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<TimeSpan, string?\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[TimeSpanToStringConverterAsset](Aspid.MVVM.StarterKit.TimeSpanToStringConverterAsset.md)

#### Implements

[IConverter\<TimeSpan, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

