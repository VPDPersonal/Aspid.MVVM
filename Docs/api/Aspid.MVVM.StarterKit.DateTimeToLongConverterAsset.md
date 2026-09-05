---
title: "Class DateTimeToLongConverterAsset"
sidebar_label: "DateTimeToLongConverterAsset"
description: "Class DateTimeToLongConverterAsset — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeToLongConverterAsset {#Aspid_MVVM_StarterKit_DateTimeToLongConverterAsset}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ConverterAsset<T1, T2>`](Aspid.MVVM.StarterKit.ConverterAsset-2.md) for [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to [`Int64`](https://learn.microsoft.com/dotnet/api/system.int64) conversions.

```csharp
[CreateAssetMenu(menuName = "Aspid/MVVM/Converters/Time/Date Time To Long Converter", fileName = "DateTimeToLongConverter")]
public sealed class DateTimeToLongConverterAsset : ConverterAsset<DateTime, long>, IConverter<DateTime, long>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ConverterAsset\<DateTime, long\>](Aspid.MVVM.StarterKit.ConverterAsset-2.md) ← 
[DateTimeToLongConverterAsset](Aspid.MVVM.StarterKit.DateTimeToLongConverterAsset.md)

#### Implements

[IConverter\<DateTime, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

