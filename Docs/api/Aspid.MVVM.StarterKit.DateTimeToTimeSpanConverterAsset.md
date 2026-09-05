---
title: "Class DateTimeToTimeSpanConverterAsset"
sidebar_label: "DateTimeToTimeSpanConverterAsset"
description: "Class DateTimeToTimeSpanConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeToTimeSpanConverterAsset {#Aspid_MVVM_StarterKit_DateTimeToTimeSpanConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To Time Span Converter", fileName = "DateTimeToTimeSpanConverter")]
public sealed class DateTimeToTimeSpanConverterAsset : ConverterAsset<DateTime, TimeSpan>, IConverter<DateTime, TimeSpan>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<DateTime, TimeSpan\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[DateTimeToTimeSpanConverterAsset](Aspid.MVVM.StarterKit.DateTimeToTimeSpanConverterAsset.md)

#### Implements

[IConverter\<DateTime, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

